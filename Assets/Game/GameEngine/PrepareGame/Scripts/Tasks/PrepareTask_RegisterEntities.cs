using GameElements;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Task «Register Entities»",
    menuName = "GameEngine/Prepare/New Task «Register Entities»"
)]
public sealed class PrepareTask_RegisterEntities : PrepareTask
{
    public override void Prepare(IGameContext gameContext)
    {
        var entitiesService = gameContext.GetService<EntitiesService>();

        var providers = FindObjectsOfType<EntitiesProvider>();
        for (int i = 0, count = providers.Length; i < count; i++)
        {
            var provider = providers[i];
            var entities = provider.ProvideEntities();
            entitiesService.AddEntities(entities);
        }
    }
}