using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : Singleton <Pathfinding>
{
    [SerializeField]private List<Unit> _units;
    [SerializeField]private bool _isAttackMode;

    public override void Init()
    {
        _units = new List<Unit>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (Unit unit in _units)
            {
                FindPath(unit.transform.position,GridManager.Instance.GetNearestAvailableNode(GetMouseWorldPoint()).WorldPos);
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            Collider2D targetInRange = Physics2D.OverlapCircle(GetMouseWorldPoint(),.05f);

            if (targetInRange&&targetInRange.TryGetComponent<Building>(out Building building))
            {
                Attack(targetInRange.transform);
            }
        }
        
    }
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = GridManager.Instance.NodeFromWorldPoint(startPos);
        Node targetNode = GridManager.Instance.NodeFromWorldPoint(targetPos);

        // Create open and closed lists
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            // Loop through openSet to find the node with the lowest fCost
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbor in GridManager.Instance.GetNeighbors(currentNode))
            {
                if (!neighbor.IsBlocked || closedSet.Contains(neighbor)) // Skip non-walkable or already processed nodes
                    continue;

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
    }
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        
        foreach (Unit unit in _units)
        {
            unit.GetComponent<Unit>().MoveAlongPath(path);
        }
        
    }
    
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs((int)nodeA.GridXY.x - (int)nodeB.GridXY.x);
        int distY = Mathf.Abs((int)nodeA.GridXY.y - (int)nodeB.GridXY.y);


        return distX + distY;
        /*if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);*/
    }

    public void Attack(Transform target)
    {
        foreach (Unit unit in _units)
        {
            
            
            FindPath(unit.transform.position, GridManager.Instance.GetNearestAvailableNode(GetMouseWorldPoint()).WorldPos);
            unit.SetTarget(target);
        }
    }
    public void UpdateUnits(GameObject[] units)
    {
        _units.Clear();

        foreach (var item in units)
        {
            _units.Add(item.GetComponent<Unit>());
        }
    }

    public Vector3 GetMouseWorldPoint()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
