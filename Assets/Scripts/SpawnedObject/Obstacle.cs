using System;
using UnityEngine;

public class Obstacle : SpawnedObject
{
    public event Action OnHalfMapReach;

    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private LoopPositionChange _loopPositionChange;

    protected ObstaclePool _pool;

    protected bool _isFree = false;

    public void Initialize(ObstaclePool pool)
    {
        _pool = pool;

        _pool.OnReturnAllToPool += Deactivate;

        CreateComplete();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (OnHalfMapReach != null && transform.position.x < 0)
        {
            OnHalfMapReach.Invoke();
            
            OnHalfMapReach = null;
        }
    }

    public override void Deactivate()
    {
        if (_isFree == true)
        {
            return;
        }

        base.Deactivate();

        OnHalfMapReach = null;

        _pool.ReturnToPool(this);
    }

    protected override void ObjectOff()
    {
        gameObject.SetActive(false);

        _isFree = true;

        trailRenderer.Clear();
    }

    protected override void ObjectOn()
    {
        Prepare();

        _isFree = false;

        gameObject.SetActive(true);
    }
}
