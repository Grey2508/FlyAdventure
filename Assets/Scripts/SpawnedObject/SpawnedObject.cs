using System;
using UnityEngine;

public abstract class SpawnedObject : MonoBehaviour
{
    public event Action OnDeactive;

    [Header("SpawnedObject")]
    [SerializeField] protected float _speed = 2f;

    [SerializeField] protected float _minX = -12f;

    protected float _currentSpeed;
    protected Vector2 _destinationVector;

    public virtual void CreateComplete()
    {
        ObjectOff();
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        transform.Translate(_destinationVector * _currentSpeed * Time.deltaTime);

        if (transform.position.x < _minX)
            Deactivate();
    }

    public virtual void Deactivate()
    {
        ObjectOff();
     
        OnDeactive?.Invoke();
    }

    public virtual void ActivateInPosition(Vector2 position)
    {
        transform.position = position;

        ObjectOn();
    }

    public virtual void Prepare()
    {
        _currentSpeed = _speed;

        _destinationVector = Vector2.left;
    }

    protected abstract void ObjectOff();
    protected abstract void ObjectOn();
}
