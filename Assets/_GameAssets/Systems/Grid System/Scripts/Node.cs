using UnityEngine;

public class Node
{
    public Vector2 GridXY;
    public Vector2 WorldPos;

    public bool IsBlocked;

    public Tile NodeTile;

    public int gCost; // Cost from the start node
    public int hCost; // Heuristic cost to the target node
    public Node parent; // Parent node for path retracing
    public int FCost
    {
        get { return gCost + hCost; }
    }

    public Node(Vector2 gridXY, Vector3 worldPos, bool isBlocked)
    {
        this.GridXY = gridXY;
        this.WorldPos = worldPos;
        this.IsBlocked = isBlocked;
    }

    public void UpdateAvailability()
    {
        IsBlocked = !Physics2D.OverlapCircle(WorldPos, GridManager.Instance.NodeSize / 2);
    }
}
