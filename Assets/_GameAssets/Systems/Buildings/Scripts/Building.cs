using UnityEngine;

public abstract class Building : MonoBehaviour
{

    [SerializeField]private protected int _health;
    public int Health => _health;

    [SerializeField] private protected BuildingData _buildingData;
    
    private SpriteRenderer _spriteRenderer;
    private Color _initialColor;

    private protected bool _placed;
    [SerializeField]private bool _canPlace = true;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialColor = _spriteRenderer.color;
        
        Preview();

        _health = _buildingData.Health;
    }



    public abstract IProduct Produce(ProductData data);

    public virtual void Preview()
    {
        _spriteRenderer.color = Color.gray;
    }
    public virtual void Place()
    {
        if (_canPlace)
        {
            _spriteRenderer.color = _initialColor;
            _placed=true;
            BuildingPlacer.Instance.IsEditMode=false;
        }

    }

    public void TakeDamage(int damage)
    {
        _health-=damage;
        _health = Mathf.Clamp(_health,0,_health);

        GetComponent<HealthUIView>().UpdateHealthText(_health);

        if (_health <= 0)
        {
            Deactivate();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Unit"))
        {
            _canPlace=false;
            _spriteRenderer.color = Color.red;
        }

    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.tag.Equals("Unit"))
        {
            _canPlace=true;
            _spriteRenderer.color = Color.white;
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        GridManager.Instance.CheckObstaclesOnNodes();
        UIManager.Instance.BuildingMenu();
    }
}
