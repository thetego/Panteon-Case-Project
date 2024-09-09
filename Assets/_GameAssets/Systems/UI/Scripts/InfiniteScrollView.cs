using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollView : MonoBehaviour
{
    [SerializeField] private RectTransform _content;       // The content RectTransform that holds the scrollable items
    [SerializeField] private ScrollRect _scrollRect;       // Reference to the scroll rect
    [SerializeField] private List<GameObject> _items;      // List of existing items already in the Content
    [SerializeField] private int _itemCount = 100;         // Total number of items to be displayed

    private float itemHeight;                             // Height of each item
    private int topIndex = 0;                             // The index of the first visible item
    private int bottomIndex = 0;                          // The index of the last visible item
    private int bufferCount;                              // The number of visible items


    IEnumerator Start()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            _items.Add(_content.transform.GetChild(i).gameObject);
        }
        yield return new WaitForSeconds(1f);
        // Calculate the height of each item from the first existing prefab
        itemHeight = _items[0].GetComponent<RectTransform>().rect.height;
        bufferCount = _items.Count;

        // Set the content height based on the number of items
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, _itemCount * itemHeight);

        // Initialize existing items with their corresponding data
        for (int i = 0; i < bufferCount; i++)
        {
            UpdateItem(i, i);
        }

        bottomIndex = bufferCount - 1;

        // Listen to the scroll event to detect when new items need to be recycled
        _scrollRect.onValueChanged.AddListener(OnScroll);
    }


    private void OnScroll(Vector2 scrollPosition)
    {
        // Check if we need to recycle items when scrolling down
        if (_scrollRect.verticalNormalizedPosition < 0.1f && bottomIndex < _itemCount - 1)
        {
            RecycleItems(1);
        }

        // Check if we need to recycle items when scrolling up
        if (_scrollRect.verticalNormalizedPosition > 0.9f && topIndex > 0)
        {
            RecycleItems(-1);
        }
    }

    private void RecycleItems(int direction)
    {
        if (direction > 0) // Scrolling down
        {
            if (bottomIndex < _itemCount - 1)
            {
                // Move the top item to the bottom
                int newTopIndex = topIndex + 1;
                int newBottomIndex = bottomIndex + 1;

                RepositionItem(newTopIndex - 1, newBottomIndex);
                topIndex = newTopIndex;
                bottomIndex = newBottomIndex;
            }
        }
        else if (direction < 0) // Scrolling up
        {
            if (topIndex > 0)
            {
                // Move the bottom item to the top
                int newBottomIndex = bottomIndex - 1;
                int newTopIndex = topIndex - 1;

                RepositionItem(newBottomIndex + 1, newTopIndex);
                topIndex = newTopIndex;
                bottomIndex = newBottomIndex;
            }
        }
    }

    private void RepositionItem(int oldIndex, int newIndex)
    {
        // Reuse the item from oldIndex and move it to newIndex
        GameObject itemToMove = _items[oldIndex % bufferCount];
        itemToMove.transform.localPosition = new Vector3(0, -newIndex * itemHeight, 0);
        UpdateItem(newIndex, newIndex);
    }

    // Update the item content (e.g., text or images) based on its new index
    private void UpdateItem(int itemIndex, int dataIndex)
    {
        //_items[itemIndex % bufferCount].GetComponentInChildren<Text>().text = "Item " + dataIndex;
    }
}
