using UnityEngine;

public class CollectItem : SpawnedObject
{
    [Header("CollectItem")]
    [SerializeField] protected int _cost = 1;

    [SerializeField] protected float _collectDistance = 0.5f;

    protected Transform _playerTransform;
    protected GameManager _gameManager;

    protected float _distance = 0;

    public virtual void Initialize(Transform playerTransform, GameManager gameManager)
    {
        _playerTransform = playerTransform;
        _gameManager = gameManager;

        CreateComplete();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        _distance = Vector2.Distance(_playerTransform.position, transform.position);

        if (_distance < _collectDistance)
            Collect();
    }

    protected virtual void Collect()
    {
        _gameManager.Collect(_cost);

        Deactivate();
    }

    protected override void ObjectOff()
    {
        gameObject.SetActive(false);
    }

    protected override void ObjectOn()
    {
        Prepare();
        gameObject.SetActive(true);
    }
}
