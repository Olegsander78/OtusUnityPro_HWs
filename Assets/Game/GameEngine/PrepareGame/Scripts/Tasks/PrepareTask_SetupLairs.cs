using Entities;
using GameSystem;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Task «Setup Lairs»",
    menuName = "GameEngine/Prepare/New Task «Setup Lairs»"
)]
public sealed class PrepareTask_SetupLairs : PrepareTask
{
    [SerializeField]
    private ScriptableEntityCondition _lairCondition;

    public override void Prepare(IGameContext gameContext)
    {
        var entitiesService = gameContext.GetService<EntitiesService>();
        var lairs = entitiesService.FindEntities(_lairCondition);

        var lairsService = gameContext.GetService<LairsService>();
        lairsService.SetupLairs(lairs);
    }
}