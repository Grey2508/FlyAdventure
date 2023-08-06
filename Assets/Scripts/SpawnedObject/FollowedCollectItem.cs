using UnityEngine;

public class FollowedCollectItem : CollectItem
{
    [Header("FollowedCollectItem")]
    [SerializeField] private float _followSpeed = 5f;

    [SerializeField] private float _followDistance = 8f;
    [SerializeField] private float _lerpSpeed = 1f;

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (_distance < _followDistance && _playerTransform.position.x < transform.position.x)
            ActivateFollowing();
        else
            DeactiveFollowing();
    }

    private void DeactiveFollowing()
    {
        Prepare();
    }

    protected virtual void ActivateFollowing()
    {
        _currentSpeed = _followSpeed;
        _destinationVector = Vector2.Lerp(_destinationVector, (_playerTransform.position - transform.position).normalized, _lerpSpeed * Time.deltaTime);
    }
}
