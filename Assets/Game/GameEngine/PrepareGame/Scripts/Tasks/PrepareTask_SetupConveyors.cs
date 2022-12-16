using Entities;
using GameElements;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Task «Setup Conveyors»",
    menuName = "GameEngine/Prepare/New Task «Setup Conveyors»"
)]
public sealed class PrepareTask_SetupConveyors : PrepareTask
{
    [SerializeField]
    private ScriptableEntityCondition conveyorCondition;

    public override void Prepare(IGameContext gameContext)
    {
        var entitiesService = gameContext.GetService<EntitiesService>();
        var conveyours = entitiesService.FindEntities(this.conveyorCondition);

        var conveyorsService = gameContext.GetService<ConveyorsService>();
        conveyorsService.SetupConveyours(conveyours);
    }
}