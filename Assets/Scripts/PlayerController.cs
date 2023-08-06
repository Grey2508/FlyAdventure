using UnityEngine;

public class PlayerController : ManagedObject
{
    [SerializeField] private float _force = 20f;
    [SerializeField] private float _rotationMultiplier = 5f;

    private Rigidbody2D _rigidbody;

    private bool _isPlaying = true;

    private GameManager _gameManager;

    private Vector2 _startPosition;

    private Transform _transform;

    public Transform Transform => _transform;

    public override void Initialize(GameManager manager)
    {
        base.Initialize(manager);

        _rigidbody = GetComponent<Rigidbody2D>();
        _gameManager = manager;

        _transform = transform;

        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_isPlaying == false)
            return;

        if (Input.GetButton("Action") == true)
        {
            _rigidbody.AddForce(new Vector2(0, _force * Time.deltaTime), ForceMode2D.Impulse);
        }

        _rigidbody.SetRotation(_rigidbody.velocity.y * _rotationMultiplier);
    }

    protected override void StartGame()
    {
        _transform.position = _startPosition;

        _isPlaying = true;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    protected override void StopGame()
    {
        _isPlaying = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _gameManager.StopGame();
    }
}
