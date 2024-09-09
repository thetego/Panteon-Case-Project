using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Buildings/DataSO", order = 0)]
public class BuildingData : ScriptableObject
{
    public string Name;
    public int Health;

    public Sprite Icon;

    public ProductData[] Products;
}
