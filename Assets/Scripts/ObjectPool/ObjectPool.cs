using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Obstacle
{
    public event System.Action OnReturnAllToPool;

    private Queue<T> _pool = new Queue<T>();

    private T _prefab;

    public void Initialize(T prefab, int count)
    {
        _prefab = prefab;

        for (int i = 0; i < count; i++)
        {
            T newObject = CreateNewItem();
            _pool.Enqueue(newObject);
        }
    }

    public T GetItem()
    {
        if (_pool.Count == 0)
        {
            T newObject = CreateNewItem();

            _pool.Enqueue(newObject);
        }

        T item = _pool.Dequeue();

        return item;
    }

    protected virtual T CreateNewItem()
    {
        T newObject = Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity);

        return newObject;
    }

    public void ReturnToPool(T item)
    {
        _pool.Enqueue(item);
    }

    public void ReturnAllToPool()
    {
        OnReturnAllToPool?.Invoke();
    }
}
