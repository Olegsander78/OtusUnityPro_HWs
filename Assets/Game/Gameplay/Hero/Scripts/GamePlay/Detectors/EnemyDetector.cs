using System.Collections.Generic;
using Entities;
using GameSystem;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Enemy Detector")]
public sealed class EnemyDetector : AbstractDetector
{
    [Space]
    [SerializeField]
    private ScriptableEntityCondition enemyCondition;

    private MeleeCombatInteractor combatInteractor;

    public override void ConstructGame(IGameContext context)
    {
        base.ConstructGame(context);
        this.combatInteractor = context.GetService<MeleeCombatInteractor>();
    }

    protected override bool MatchesEntity(IEntity entity)
    {
        return this.enemyCondition.IsTrue(entity);
    }

    protected override void OnEntitesChanged(List<IEntity> entities)
    {
        if (entities.Count > 0)
        {
            var targetEnemy = entities[0];
            this.combatInteractor.TryStartCombat(targetEnemy);
        }
    }
}