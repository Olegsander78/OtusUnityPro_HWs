using GameElements;
using UnityEngine;


public abstract class ChestRewardObserver : MonoBehaviour,
        IGameInitElement,
        IGameReadyElement,
        IGameFinishElement
{
    private ChestsManager _chestsManager;
    public virtual void InitGame(IGameContext context)
    {
        _chestsManager = context.GetService<ChestsManager>();
    }

    public virtual void ReadyGame(IGameContext context)
    {
        _chestsManager.OnRewardGenerated += OnRewardRecieved;
    }
    public virtual void FinishGame(IGameContext context)
    {
        _chestsManager.OnRewardGenerated -= OnRewardRecieved;
    }

    protected abstract void OnRewardRecieved(ChestRewardConfig chestRewardConfig);
}