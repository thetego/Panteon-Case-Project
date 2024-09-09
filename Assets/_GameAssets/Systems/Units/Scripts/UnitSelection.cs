using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private bool _isDragging = false;
    private List<GameObject> _selectedUnits = new List<GameObject>();

    void Update()
    {
        // Start dragging with left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _startPosition = Input.mousePosition;
        }

        // Update the drag position
        if (Input.GetMouseButton(0))
        {
            _endPosition = Input.mousePosition;
        }

        // Finish the selection when the button is released
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            SelectUnits();
        }
    }

    void OnGUI()
    {
        if (_isDragging)
        {
            // Create a rectangle based on start and end positions
            var rect = GetScreenRect(_startPosition, _endPosition);
            DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));  // Selection box fill
            DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));  // Selection box border
        }
    }

    private void SelectUnits()
    {
        _selectedUnits.Clear();

        // Get the rectangular selection area in world space
        var selectionRect = GetViewportRect(_startPosition, _endPosition);

        // Iterate over all selectable units
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            Vector3 unitViewportPosition = Camera.main.WorldToViewportPoint(unit.transform.position);

            // Check if the unit falls within the selection rectangle
            if (selectionRect.Contains(unitViewportPosition))
            {
                _selectedUnits.Add(unit);
                unit.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                unit.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        Pathfinding.Instance.UpdateUnits(_selectedUnits.ToArray());
    }

    private Rect GetScreenRect(Vector2 screenPosition1, Vector2 screenPosition2)
    {
        // Move from screen space to a normalized rectangle
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        Vector2 topLeft = Vector2.Min(screenPosition1, screenPosition2);
        Vector2 bottomRight = Vector2.Max(screenPosition1, screenPosition2);
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    private Rect GetViewportRect(Vector2 screenPosition1, Vector2 screenPosition2)
    {
        // Convert screen rect to viewport rect
        Vector2 viewportPosition1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        Vector2 viewportPosition2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        Vector2 bottomLeft = Vector2.Min(viewportPosition1, viewportPosition2);
        Vector2 topRight = Vector2.Max(viewportPosition1, viewportPosition2);
        return Rect.MinMaxRect(bottomLeft.x, bottomLeft.y, topRight.x, topRight.y);
    }

    private void DrawScreenRect(Rect rect, Color color)
    {
        // Draws a filled rectangle in screen space
        GUI.color = color;
        GUI.DrawTexture(rect, Texture2D.whiteTexture);
        GUI.color = Color.white;
    }

    private void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Draws a rectangle border in screen space
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);  // Top
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);  // Left
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);  // Bottom
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);  // Right
    }
}

