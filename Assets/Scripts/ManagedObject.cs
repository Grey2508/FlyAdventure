using UnityEngine;

public abstract class ManagedObject : MonoBehaviour
{
    public virtual void Initialize(GameManager manager)
    {
        manager.OnStartGame += StartGame;
        manager.OnStopGame += StopGame;
    }

    protected virtual void Terminate(GameManager manager)
    {
        manager.OnStartGame -= StartGame;
        manager.OnStopGame -= StopGame;
    }

    protected abstract void StopGame();

    protected abstract void StartGame();
}
