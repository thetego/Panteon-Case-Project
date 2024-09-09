using UnityEngine;

[CreateAssetMenu(fileName ="SoldierData",menuName ="Soldiers/DataSO",order = 0)]
public class SoldierData : ScriptableObject
{
    public string Name;
    public int Health;
    public int DamagePower;
    public float AttackRange;
    public float AttackRate;

    public float ProduceTime;

    public Sprite SoldierSprite;
}
