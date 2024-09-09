using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    [SerializeField]private GameObject _productionMenuUI,_buildingMenuUI;
    [SerializeField]private ProductionMenu _productionMenu;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildingMenu();
        }
    }

    public void ProductionMenu(BuildingData data, Building building)
    {
        _productionMenuUI.SetActive(true);
        _buildingMenuUI.SetActive(false);

        _productionMenu.InitializeProduct(building,data);
    }
    public void BuildingMenu()
    {
        _productionMenuUI.SetActive(false);
        _buildingMenuUI.SetActive(true);
    }

}
