using System.Collections;
using UnityEngine;

public class LoopPositionChange : MonoBehaviour
{
    [SerializeField] protected float _speed = 5;

    [SerializeField] protected Vector2 _startPosition;
    [SerializeField] protected Vector2 _endPosition;

    [SerializeField] protected bool _isLocalPosition = true;

    [SerializeField, Min(0)] protected float _pauseOnEnds = 0;

    [SerializeField, Range(0, 1)] protected float _persentRandomRange = 0.5f;

    protected float _defaultSpeed;

    protected Vector2 _defaultStartPosition;
    protected Vector2 _defaultEndPosition;

    protected float _defaultPauseOnEnds;

    private void Awake()
    {
        _defaultSpeed = _speed;
        _defaultStartPosition = _startPosition;
        _defaultEndPosition = _endPosition;
        _defaultPauseOnEnds = _pauseOnEnds;
    }

    private void OnEnable()
    {
        StartCoroutine(Moving());

        SetRandomValues();
    }

    private void SetRandomValues()
    {
        _speed = _defaultSpeed + _defaultSpeed * Random.Range(-_persentRandomRange, _persentRandomRange);
        _startPosition.y = _defaultStartPosition.y + _defaultStartPosition.y * Random.Range(-_persentRandomRange, _persentRandomRange);
        _endPosition.y = _defaultEndPosition.y + _defaultEndPosition.y * Random.Range(-_persentRandomRange, _persentRandomRange);
        _pauseOnEnds = _defaultPauseOnEnds + _defaultPauseOnEnds * Random.Range(-_persentRandomRange, _persentRandomRange);
    }

    protected IEnumerator Moving()
    {
        while (true)
        {
            yield return MoveForward();

            yield return new WaitForSeconds(CalculatePause(_pauseOnEnds));

            yield return MoveBackward();

            yield return new WaitForSeconds(CalculatePause(_pauseOnEnds));
        }
    }

    protected IEnumerator MoveForward()
    {
        StartMoveForward();

        for (float t = 0; t < 1; t += Time.deltaTime * _speed)
        {
            Vector3 newPosition = Vector3.Lerp(_startPosition, _endPosition, t);

            if (_isLocalPosition)
                transform.localPosition = newPosition;
            else
                transform.position = newPosition;

            yield return null;
        }

        EndMoveForward();
    }

    protected virtual void StartMoveForward()
    {
    }

    protected virtual void EndMoveForward()
    {
        if (_isLocalPosition)
            transform.localPosition = _endPosition;
        else
            transform.position = _endPosition;
    }

    protected IEnumerator MoveBackward()
    {
        StartMoveBackward();

        for (float t = 1; t > 0; t -= Time.deltaTime * _speed)
        {
            Vector3 newPosition = Vector3.Lerp(_startPosition, _endPosition, t);

            if (_isLocalPosition)
                transform.localPosition = newPosition;
            else
                transform.position = newPosition;

            yield return null;
        }

        EndMoveBackward();
    }

    protected virtual void StartMoveBackward()
    {
    }

    protected virtual void EndMoveBackward()
    {
        if (_isLocalPosition)
            transform.localPosition = _startPosition;
        else
            transform.position = _startPosition;
    }

    protected float CalculatePause(float targetDelay)
    {
        return targetDelay;
    }
}
