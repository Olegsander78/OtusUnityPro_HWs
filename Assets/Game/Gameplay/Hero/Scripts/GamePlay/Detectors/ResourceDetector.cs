using System.Collections.Generic;
using Entities;
using GameSystem;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Resource Detector")]
public sealed class ResourceDetector : AbstractDetector
{
    [Space]
    [SerializeField]
    private ScriptableEntityCondition resourceCondition;

    private HarvestResourceInteractor resourceInteractor;

    protected override bool MatchesEntity(IEntity entity)
    {
        return this.resourceCondition.IsTrue(entity);
    }

    public override void ConstructGame(IGameContext context)
    {
        base.ConstructGame(context);
        this.resourceInteractor = context.GetService<HarvestResourceInteractor>();
    }

    protected override void OnEntitesChanged(List<IEntity> entities)
    {
        if (entities.Count > 0)
        {
            var targetResource = entities[0];
            this.resourceInteractor.StartHarvest(targetResource);
        }
    }
}