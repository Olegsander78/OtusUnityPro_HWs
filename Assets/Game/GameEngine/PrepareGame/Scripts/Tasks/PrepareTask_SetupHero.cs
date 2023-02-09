using Entities;
using GameSystem;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Task «Setup Hero»",
    menuName = "GameEngine/Prepare/New Task «Setup Hero»"
)]
public sealed class PrepareTask_SetupHero : PrepareTask
{
    [SerializeField]
    private ScriptableEntityCondition heroCondition;

    public override void Prepare(IGameContext gameContext)
    {
        var entitiesService = gameContext.GetService<EntitiesService>();
        if (entitiesService.FindEntity(this.heroCondition, out IEntity hero))
        {
            var heroService = gameContext.GetService<HeroService>();
            heroService.SetupHero((UnityEntity)hero);
        }
    }
}