using UnityEngine;

public class GameOverScreen : BaseScreen
{
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI _timeLeftText;

    [SerializeField] private float _minShowTime = 0.5f;

    private float _startShowTime = 0;

    public void ShowScreen(int scores)
    {
        _scoreText.text = $"Scores: {scores}";

        _timeLeftText.text = $"Time left: {(Time.realtimeSinceStartup - _gameManager.GameStartTime).ToString("00.00")}";

        ShowScreen();

        _startShowTime = Time.realtimeSinceStartup;
    }

    public override void HideScreen()
    {
        if (Time.realtimeSinceStartup - _startShowTime < _minShowTime)
            return;

        base.HideScreen();
    }

    protected override void StopGame()
    {
        base.StopGame();

        ShowScreen();
    }
}
