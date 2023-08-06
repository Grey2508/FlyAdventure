using System;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Played,
    Stopped
}

public class GameManager : MonoBehaviour
{
    public event Action OnStartGame;
    public event Action OnStopGame;
    public event Action<int> OnCollectItem;

    [SerializeField] private List<ManagedObject> _manageObjects;

    private float _gameStartTime = 0;

    private PlayerController _playerController;

    public PlayerController PlayerController => _playerController;

    public float GameStartTime => _gameStartTime;

    private void Start()
    {
        for(int i=0; i < _manageObjects.Count; i++)
        {
            _manageObjects[i].Initialize(this);

            if (_manageObjects[i] is PlayerController)
                _playerController = (PlayerController)_manageObjects[i];
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;

        OnStartGame.Invoke();

        _gameStartTime = Time.realtimeSinceStartup;
    }

    public void StopGame()
    {
        Time.timeScale = 0;

        OnStopGame.Invoke();
    }

    internal void Collect(int score)
    {
        OnCollectItem(score);
    }
}
