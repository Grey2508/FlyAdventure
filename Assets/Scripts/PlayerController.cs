using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _force = 20f;
    [SerializeField] private KeyCode[] _controlKeys;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_controlKeys.Length == 0)
            return;

        for (int i = 0; i < _controlKeys.Length; i++)
        {
            if (Input.GetKey(_controlKeys[i]))
            {
                _rigidbody.AddForce(new Vector2(0, _force * Time.deltaTime), ForceMode2D.Impulse);
                return;
            }
        }
    }
}
