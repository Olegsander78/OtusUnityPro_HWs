using Entities;
using GameSystem;
using UnityEngine;


public sealed class EntitySpawner : MonoBehaviour, IGameAttachElement
{
    private IGameContext gameContext;

    public UnityEntity SpawnEntity(UnityEntity prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        var entity = Instantiate(prefab, position, rotation, parent);
        if (entity.TryGetComponent(out IGameElement gameElement))
        {
            this.gameContext.RegisterElement(gameElement);
        }

        return entity;
    }

    void IGameAttachElement.AttachGame(IGameContext context)
    {
        this.gameContext = context;
    }
}