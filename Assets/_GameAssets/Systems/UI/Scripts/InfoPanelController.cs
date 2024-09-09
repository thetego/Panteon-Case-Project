using System.Linq;
using UnityEngine;

public class InfoPanelController : Singleton<InfoPanelController>
{
    [SerializeField] private InfoPanelView _infoPanelView; // Reference to the view

    // Method to initialize the information panel
    public void InitializePanel(BuildingData buildingData)
    {
        // Update the main building info
        _infoPanelView.UpdateBuildingView(buildingData.Name, buildingData.Icon);

        // Update the product list
        _infoPanelView.UpdateProductList(buildingData.Products.ToList());

        
    }
}
