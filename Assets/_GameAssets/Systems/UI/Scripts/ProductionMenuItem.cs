using UnityEngine;
using UnityEngine.UI;

public class ProductionMenuItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _productPrefab;
    private ProductData _productData;
    private Building _building;

    public void Initialize(Sprite sprite, ProductData data, Building building)
    {
        _image.sprite = sprite;
        _productPrefab = data.ProductPrefab;
        _productData = data;
        _building = building;

        GetComponent<Button>().onClick.AddListener(StartProduce);

    }

    public void StartProduce()
    {
        _building.Produce(_productData);
    }

}
