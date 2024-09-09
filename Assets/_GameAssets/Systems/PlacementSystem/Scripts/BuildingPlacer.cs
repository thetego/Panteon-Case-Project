using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : Singleton<BuildingPlacer>
{
    [SerializeField] private GameObject _barrackPrefab, _powerPlant, _housePrefab;
    [SerializeField] private GameObject _previewObject;

    public bool IsEditMode;

    void Update()
    {
        if (_previewObject != null && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            _previewObject.GetComponent<Building>().Place();
            GridManager.Instance.CheckObstaclesOnNodes();
            _previewObject = null;
        }
    }

    public void PlaceBarrack()
    {
        Building barrack = Instantiate(_barrackPrefab).GetComponent<Barrack>();
        _previewObject = barrack.gameObject;
        IsEditMode=true;
    }
    public void PlacePowerplant()
    {
        Building powerPlant = Instantiate(_powerPlant).GetComponent<Powerplant>();
        _previewObject = powerPlant.gameObject;
        IsEditMode=true;
    }
    public void PlaceHouse()
    {
        Building house = Instantiate(_housePrefab).GetComponent<House>();
        _previewObject = house.gameObject;
        IsEditMode=true;
    }
    private void CancelPlacing()
    {
        Destroy(_previewObject);
    }
}
