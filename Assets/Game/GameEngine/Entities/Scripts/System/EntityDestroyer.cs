using Entities;
using GameElements;
using UnityEngine;


public sealed class EntityDestroyer : MonoBehaviour, IGameAttachElement
{
    private static readonly Vector3 OUTSCENE_POSITION = new Vector3(10000, 10000, 10000);

    private IGameContext gameContext;

    public void DestroyEntity(UnityEntity entity)
    {
        if (entity.TryGetComponent(out IGameElement gameElement))
        {
            this.gameContext.UnregisterElement(gameElement);
        }

        entity.transform.position = OUTSCENE_POSITION;
        Destroy(entity.gameObject, 0.1f);
    }

    void IGameAttachElement.AttachGame(IGameContext context)
    {
        this.gameContext = context;
    }
}