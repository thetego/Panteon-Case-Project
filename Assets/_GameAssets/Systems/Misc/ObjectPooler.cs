using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler<T> where T : MonoBehaviour
{
    private Queue<T> _objectPool;
    private T _objectPrefab;
    private Transform _parentTransform;

    public ObjectPooler(T prefab, int initialSize, Transform parent = null)
    {
        _objectPool = new Queue<T>();
        _objectPrefab = prefab;
        _parentTransform = parent;

        // Instantiate the initial pool of objects
        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(_objectPrefab, _parentTransform);
            obj.gameObject.SetActive(false); // Deactivate initially
            _objectPool.Enqueue(obj);
        }
    }

    // Method to retrieve an object from the pool
    public T SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        if (_objectPool.Count == 0)
        {
            // If the pool is empty, instantiate a new object
            T obj = Object.Instantiate(_objectPrefab, position, rotation, _parentTransform);
            return obj;
        }

        // Otherwise, dequeue an object from the pool
        T objectToSpawn = _objectPool.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.gameObject.SetActive(true); // Activate the object

        return objectToSpawn;
    }

    // Method to return an object back to the pool
    public void ReturnToPool(T objectToReturn)
    {
        _objectPool.Enqueue(objectToReturn); 
    }

    // Reset all objects in the pool (deactivate them)
    public void ResetPool()
    {
        foreach (T obj in _objectPool)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
