using UnityEngine;

public class BaseScreen : ManagedObject
{
    protected GameManager _gameManager;

    private void Update()
    {
        if (Input.anyKeyDown)
            HideScreen();
    }

    public virtual void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideScreen()
    {
        gameObject.SetActive(false);

        _gameManager.StartGame();
    }
    public override void Initialize(GameManager manager)
    {
        base.Initialize(manager);

        _gameManager = manager;
    }

    protected override void StopGame()
    {
    }

    protected override void StartGame()
    {
    }
}
