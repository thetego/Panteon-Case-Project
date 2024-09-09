using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "Products/DataSO", order = 0)]
public class ProductData : ScriptableObject
{
    public GameObject ProductPrefab;
    public Sprite Icon;
    public SoldierData Data;
}
