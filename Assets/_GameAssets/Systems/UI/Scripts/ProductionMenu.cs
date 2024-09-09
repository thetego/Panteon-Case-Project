using UnityEngine;

public class ProductionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _productionItemUIPrefab;
    

    public void InitializeProduct(Building building,BuildingData buildingData)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach (var item in buildingData.Products)
        {
            Instantiate(_productionItemUIPrefab,this.transform).GetComponent<ProductionMenuItem>().Initialize(item.Icon,item,building);
        }
    }
}
