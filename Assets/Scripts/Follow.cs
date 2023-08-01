using UnityEngine;

[ExecuteAlways]
public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpRate = 5;

    [SerializeField] private bool _isUseLerp = false;

    void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 newPosition = new Vector2(_target.position.x, 0);

        if (_isUseLerp)
        {
            newPosition = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * _lerpRate);
        }

        transform.position = newPosition;
    }
}