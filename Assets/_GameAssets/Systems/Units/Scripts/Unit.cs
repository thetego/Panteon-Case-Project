using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private protected SoldierData _data;

    private protected int _health;

    private protected Transform _target;

    public int Health => _health;
    public SoldierData Data => _data;

    public float speed = 5f;
    private List<Node> path;
    private int currentPathIndex = 0;

    public void MoveAlongPath(List<Node> newPath)
    {
        path = newPath;
        currentPathIndex = 0;
        StopCoroutine("FollowPathCoroutine");
        StartCoroutine("FollowPathCoroutine");
    }

    public void CancelPath()
    {
        StopCoroutine("FollowPathCoroutine");
    }

    IEnumerator FollowPathCoroutine()
    {
        while (currentPathIndex < path.Count)
        {
            Vector3 targetPosition = path[currentPathIndex].WorldPos;
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            currentPathIndex++;
        }
    }

    public void SetData(SoldierData data)
    {
        _data=data;
    }
    public void SetTarget(Transform newTarget)
    {
        _target=newTarget;
    }
    public void TakeDamage(int damage)
    {
        _health -= 0;
        _health-=damage;
        _health = Mathf.Clamp(_health,0,_health);


        if (_health <= 0)
        {
            Deactivate();
        }
    }

    public void Attack()
    {
        CancelPath();
        _target.GetComponent<Building>().TakeDamage(_data.DamagePower);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        _health = _data.Health;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        GameManager.Instance.UnitPool.ReturnToPool(this);
    }
}
