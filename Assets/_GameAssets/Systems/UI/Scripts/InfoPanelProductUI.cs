using UnityEngine;
using UnityEngine.UI;

public class InfoPanelProductUI : MonoBehaviour
{
    [SerializeField] private Image productImage;

    // Method to set the product information
    public void SetProductInfo(string productName, Sprite productSprite)
    {
        productImage.sprite = productSprite;
    }
}
