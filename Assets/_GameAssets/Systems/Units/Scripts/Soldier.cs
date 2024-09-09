using UnityEngine;

public class Soldier : Unit, IProduct, IPoolable
{

    private SpriteRenderer _spriteRenderer;

    private float _lastAttackTime;


    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Initialize()
    {   
        _health = Data.Health;
        _spriteRenderer.sprite = _data.SoldierSprite;
    }


    void Update()
    {
        if (_target != null)
        {
            // Check the distance between the unit and the target
            float distanceToTarget = Vector2.Distance(transform.position, _target.position);

            // If the target is within the attack range, attack
            if (distanceToTarget <= _data.AttackRange)
            {
                // Check if enough time has passed to perform another attack
                if (Time.time >= _lastAttackTime + _data.AttackRate)
                {
                    Attack();
                    _lastAttackTime = Time.time;
                }
            }
        }
    }

    public void OnSpawn()
    {
        Initialize();
    }

}
