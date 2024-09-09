using UnityEngine;

public class Powerplant : Building
{
  public override IProduct Produce(ProductData data)
    {
        throw new System.NotImplementedException();
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
            InfoPanelController.Instance.InitializePanel(_buildingData);
        }
            
    }

}
