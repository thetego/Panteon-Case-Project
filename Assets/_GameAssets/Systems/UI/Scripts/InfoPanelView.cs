using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text buildingNameText;
    [SerializeField] private Image buildingImage;
    [SerializeField] private Transform productsContainer; // Parent container for product list (e.g., Vertical Layout Group)
    [SerializeField] private GameObject productPrefab; // Prefab for individual product UI element

    // Method to update the building view
    public void UpdateBuildingView(string buildingName, Sprite buildingSprite)
    {
        buildingNameText.text = buildingName;
        buildingImage.sprite = buildingSprite;
    }

    // Method to dynamically update product list
    public void UpdateProductList(List<ProductData> products)
    {
        // Clear existing products
        foreach (Transform child in productsContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a UI element for each product
        foreach (var product in products)
        {
            GameObject productUI = Instantiate(productPrefab, productsContainer);
            InfoPanelProductUI productComponent = productUI.GetComponent<InfoPanelProductUI>();
            productComponent.SetProductInfo(product.name, product.Icon);
        }
    }
}
