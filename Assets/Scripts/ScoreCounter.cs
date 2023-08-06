using UnityEngine;

public class ScoreCounter : ManagedObject
{
    [SerializeField] TMPro.TextMeshProUGUI _scoreText;

    [SerializeField] GameOverScreen _gameOverScreen;

    private int _score;

    public int Score => _score;

    public override void Initialize(GameManager manager)
    {
        base.Initialize(manager);

        manager.OnCollectItem += AddScores;
    }

    protected override void Terminate(GameManager manager)
    {
        base.Terminate(manager);

        manager.OnCollectItem -= AddScores;
    }

    public void AddScores(int value)
    {
        _score += value;

        SetScoreText();
    }

    private void SetScoreText()
    {
        _scoreText.text = _score.ToString();
    }

    protected override void StopGame()
    {
        _gameOverScreen.ShowScreen(_score);
    }

    protected override void StartGame()
    {
        _score = 0;
        SetScoreText();
    }
}
