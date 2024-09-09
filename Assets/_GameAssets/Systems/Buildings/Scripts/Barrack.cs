using UnityEngine;

public class Barrack : Building
{
    [SerializeField] private Transform _spawnPoint;

    public override IProduct Produce(ProductData data)
    {
        //Soldier soldier = Instantiate(data.ProductPrefab,_spawnPoint.position,Quaternion.identity).GetComponent<Soldier>();
        Soldier soldier = GameManager.Instance.UnitPool.SpawnFromPool(_spawnPoint.position, Quaternion.identity)as Soldier;
        soldier.SetData(data.Data);
        soldier.OnSpawn();

        return soldier;
    }


    void Update()
    {
        if (!_placed)
        {
            GridSnapMovement();
        }
    }

    private void GridSnapMovement()
    {
        Vector2 mousePos = Input.mousePosition;

        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);

        float snappedX = Mathf.RoundToInt(pos.x/.32f)*.35f;
        float snappedY = Mathf.RoundToInt(pos.y/.32f)*.35f;

        transform.position = new Vector3(snappedX,snappedY,transform.position.z);
    }

    void OnMouseDown()
    {
        if (_placed)
        {
            UIManager.Instance.ProductionMenu(_buildingData,this);
            InfoPanelController.Instance.InitializePanel(_buildingData);
        }
            
    }
    

}
