using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ChestFactory : MonoBehaviour, IGameAttachElement
{
    private IGameContext _gameContext;

    [Button]
    public Chest CreateChest(ChestConfig config)
    {
        var chest = config.InstantiateChest(context: this);
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