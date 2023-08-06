using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : ManagedObject
{
    [SerializeField] private CollectItem[] _collectItems;
    [SerializeField] private Obstacle[] _obstaclesPrefabs;

    [SerializeField] private int _startPoolSize = 7;
    #region SpawnArea
    [SerializeField, HideInInspector] private List<Vector2> _points;

    private float _left = 0;
    private float _right = 0;
    private float _top = 0;
    private float _bottom = 0;
    #endregion

    private bool _isSpawned = false;

    private int _obstaclesCount = 1;
    private ObstaclePool[] _obstaclesPools;
    private int _obstaclesReachHalfMapCount = 0;
    private int _obstaclesCheckCount = 0;

    public int PointsCount => _points.Count;

    public Vector2 GetPoint(int index) => _points[index];

    public void SetPoint(int index, Vector2 point)
    {
        _points[index] = point;
    }

    public void AddPoint()
    {
        Vector2 point = Vector2.zero;

        if (PointsCount == 0)
            point = transform.position;
        else
            point = GetPoint(PointsCount - 1);

        point.y += 1;

        _points.Add(point);
    }

    public void RemovePoint()
    {
        if (PointsCount == 0)
            return;

        _points.RemoveAt(PointsCount - 1);
    }

    public override void Initialize(GameManager manager)
    {
        base.Initialize(manager);

        manager.OnCollectItem += AddObstacles;

        _isSpawned = false;

        for (int i = 0; i < _collectItems.Length; i++)
        {
            _collectItems[i].Initialize(manager.PlayerController.Transform, manager);
            _collectItems[i].OnDeactive += SpawnCollectItem;
        }

        _obstaclesPools = new ObstaclePool[_obstaclesPrefabs.Length];

        for (int i = 0; i < _obstaclesPools.Length; i++)
        {
            _obstaclesPools[i] = new ObstaclePool();
            _obstaclesPools[i].Initialize(_obstaclesPrefabs[i], _startPoolSize);
        }

        CalculateBounds();
    }

    protected override void Terminate(GameManager manager)
    {
        base.Terminate(manager);

        manager.OnCollectItem -= AddObstacles;

        for (int i = 0; i < _collectItems.Length; i++)
        {
            _collectItems[i].OnDeactive -= SpawnCollectItem;
        }
    }

    private void SpawnObstacles()
    {
        if (_isSpawned == false)
            return;

        _obstaclesReachHalfMapCount = 0;
        _obstaclesCheckCount = _obstaclesCount;

        for (int i = 0; i < _obstaclesCount; i++)
        {
            int type = Random.Range(0, _obstaclesPrefabs.Length);

            Vector2 startPoint = GenerateSpawnPoint();

            Obstacle newObstacle = _obstaclesPools[type].GetItem();

            newObstacle.ActivateInPosition(startPoint);

            newObstacle.OnHalfMapReach += ObstacleReachHalfMap;
        }

    }

    private void ObstacleReachHalfMap()
    {
        _obstaclesReachHalfMapCount++;

        if (_obstaclesReachHalfMapCount == _obstaclesCheckCount)
            SpawnObstacles();
    }

    protected override void StartGame()
    {
        for (int i = 0; i < _obstaclesPools.Length; i++)
        {
            _obstaclesPools[i].ReturnAllToPool();
        }

        for(int i =0; i<_collectItems.Length; i++)
        {
            _collectItems[i].Deactivate();
        }

        _isSpawned = true;

        SpawnCollectItem();

        _obstaclesCount = 1;
        SpawnObstacles();
    }

    protected override void StopGame()
    {
        _isSpawned = false;
    }

    protected void AddObstacles(int value)
    {
        _obstaclesCount += value;
    }

    protected void SpawnCollectItem()
    {
        if (_isSpawned == false)
            return;

        int type = Random.Range(0, _collectItems.Length);

        Vector2 startPoint = GenerateSpawnPoint();

        CollectItem newCollectItem = _collectItems[type];

        newCollectItem.ActivateInPosition(startPoint);
    }

    protected Vector2 GenerateSpawnPoint()
    {
        Vector2 point = transform.position;

        if (_points.Count == 0)
            return point;

        point.x = Random.Range(_left, _right);
        point.y = Random.Range(_top, _bottom);

        return point;
    }

    private void CalculateBounds()
    {
        _left = float.PositiveInfinity;
        _right = float.NegativeInfinity;
        _top = float.NegativeInfinity;
        _bottom = float.PositiveInfinity;

        for (int i = 0; i < PointsCount; i++)
        {
            var point = GetPoint(i);

            if (_left > point.x)
                _left = point.x;
            if (_right < point.x)
                _right = point.x;
            if (_top < point.y)
                _top = point.y;
            if (_bottom > point.y)
                _bottom = point.y;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        DrawAreas();
    }

    protected virtual void DrawAreas()
    {
        Handles.color = Color.yellow;

        for (int i = 0; i < PointsCount; i++)
        {
            int nextIndex = i == PointsCount - 1 ? 0 : i + 1;
            Handles.DrawLine(GetPoint(i), GetPoint(nextIndex));
        }
    }
#endif
}
