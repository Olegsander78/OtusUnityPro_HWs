using GameElements;
using UnityEngine;


public sealed class ChestFactory : MonoBehaviour, IGameAttachElement
{
    private IGameContext _gameContext;
       
    public Chest CreateChest(ChestConfig config)
    {
        var chest = config.InstantiateChest(context: this);
        chest.Reward = chest.GenerateReward();
        Debug.Log($"{chest.Reward} generated!");
        if (chest is IGameElement gameElement)
        {
            _gameContext.RegisterElement(gameElement);
        }

        return chest;
    }

    public void DisposeChest(Chest chest)
    {
        if (chest is IGameElement gameElement)
        {
            _gameContext.UnregisterElement(gameElement);
        }
    }

    void IGameAttachElement.AttachGame(IGameContext context)
    {
        _gameContext = context;
    }
}