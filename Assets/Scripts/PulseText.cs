using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class PulseText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private int _maxSize = 85;
    [SerializeField] private int _minSize = 55;
    [SerializeField] private int _speed = 75;
    [SerializeField] private float _duration = 0;
    [SerializeField] private bool _playOnEnable = true;

    private float _timer;
    private bool _bigger = true;

    private bool _buzzy = false;

    private Color _targetColor = Color.red;
    private bool _isColorChanging = false;

    private void OnEnable()
    {
        if (_playOnEnable)
            StartEffect();
    }

    public void StartEffect(string changingText = "")
    {
        if (_buzzy == false)
            StartCoroutine(ShowEffectCoroutine(changingText));
    }

    public void SetDuration(float duration)
    {
        _duration = duration;
    }

    public void SetColor(Color color)
    {
        _targetColor =color;
        _isColorChanging = true;
    }

    private IEnumerator ShowEffectCoroutine(string changingText = "")
    {
        _buzzy = true;

        float duration = 0;

        bool defaultBestFit = _text.autoSizeTextContainer;
        _text.autoSizeTextContainer = false;

        string defaultText = _text.text;

        if (string.IsNullOrWhiteSpace(changingText) == false)
        {
            _text.text = changingText;
        }

        Color defaultColor = _text.color;
        if(_isColorChanging == true)
        {
            _text.color = _targetColor;
        }

        while (true)
        {
            if (_duration > 0)
            {
                duration += Time.deltaTime;
                if (duration > _duration)
                    break;
            }

            _timer += Time.deltaTime;

            int size = Mathf.FloorToInt(_bigger ? _minSize + _timer * _speed : _maxSize - _timer * _speed);
            _text.fontSize = size;

            if (size > _maxSize || size < _minSize)
            {
                _bigger = !_bigger;

                _timer = 0;
            }

            yield return null;
        }

        _text.autoSizeTextContainer = defaultBestFit;
        _text.text = defaultText;
        _text.color = defaultColor;

        _buzzy = false;
    }
}
