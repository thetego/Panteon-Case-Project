using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{

    [SerializeField] private Vector2 _gridSize;
    [SerializeField,Range(0.0f,3f)] private float _nodeSize, _nodeDiameter;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private LayerMask _mask;
    
    public Node[,] Nodes;
    private List<Tile> _tiles;

    public Vector2 GridSize => _gridSize;
    public float NodeSize => _nodeSize;
    public float NodeDiameter=>_nodeDiameter;

    int xS;
    int yS;

    void Start()
    {
        xS = Mathf.RoundToInt(_gridSize.x/_nodeDiameter);
        yS = Mathf.RoundToInt(_gridSize.y/_nodeDiameter);
        GenerateGrid();
    }
    
    private void GenerateGrid()
    {
        Nodes = new Node[xS,yS];
        _tiles=new List<Tile>();

        Vector3 startPoint = transform.position - Vector3.right * _gridSize.x /2 - Vector3.up * _gridSize.y / 2;

        for (int x = 0; x < xS; x++)
        {
            for (int y = 0; y < yS; y++)
            {
                Vector3 worldPoint = startPoint + Vector3.right * (x * _nodeDiameter + _nodeSize) + Vector3.up * (y * _nodeDiameter + _nodeSize);

                Nodes[x, y] = new Node(new Vector2(x,y),worldPoint,!Physics2D.OverlapCircle(worldPoint, _nodeSize / 2));
            } 
        }
    }

    public void CheckObstaclesOnNodes()
    {
        foreach (Node node in Nodes)
        {
            node.UpdateAvailability();
        }
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // Calculate the percentage across the grid's x and y axes
        float percentX = (worldPosition.x - (transform.position.x - _gridSize.x / 2)) / _gridSize.x;
        float percentY = (worldPosition.y - (transform.position.y - _gridSize.y / 2)) / _gridSize.y;

        // Clamp the percentages to ensure they are between 0 and 1
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // Calculate the grid indices based on the percentage of the position in the grid
        int x = Mathf.FloorToInt((xS) * percentX);
        int y = Mathf.FloorToInt((yS) * percentY);

        // Return the corresponding node
        return Nodes[x, y];
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // Loop through the surrounding nodes (-1 to 1 for x and y to include diagonal neighbors)
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // Skip the current node itself
                if (x == 0 && y == 0)
                    continue;

                int checkX = (int)node.GridXY.x + x;
                int checkY = (int)node.GridXY.y + y;

                // Check if the neighbor is within the grid bounds
                if (checkX >= 0 && checkX < xS && checkY >= 0 && checkY < yS)
                {
                    neighbors.Add(Nodes[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }
    public Node GetNearestAvailableNode(Vector3 targetWorldPosition)
    {
        Node targetNode = NodeFromWorldPoint(targetWorldPosition);

        if (targetNode.IsBlocked)
        {
            return targetNode;
        }

        int searchRadius = 1;
        while (searchRadius < Mathf.Max(xS, yS))
        {
            // Check the surrounding nodes in a square (expanding outward)
            for (int x = -searchRadius; x <= searchRadius; x++)
            {
                for (int y = -searchRadius; y <= searchRadius; y++)
                {
                    int checkX = (int)targetNode.GridXY.x + x;
                    int checkY = (int)targetNode.GridXY.y + y;

                    // Make sure the node is within the grid bounds
                    if (checkX >= 0 && checkX < xS && checkY >= 0 && checkY < yS)
                    {
                        Node neighborNode = Nodes[checkX, checkY];
                        if (neighborNode.IsBlocked)
                        {
                            // If we find a walkable node, return it
                            return neighborNode;
                        }
                    }
                }
            }

            
            searchRadius++;
        }

        // If no walkable node is found (which shouldn't happen unless the grid is full of obstacles)
        return null;
    }
    void OnDrawGizmos()
    {
        foreach (Node item in Nodes)
        {
            Gizmos.DrawCube(item.WorldPos,Vector2.one*_nodeSize);
        }
    }
}
