using System;
using System.Collections.Generic;
using Entities;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class EnemyDetectController : MonoBehaviour
{
    [Space]
    [SerializeField]
    private ScriptableEntityCondition _enemyCondition;

    [SerializeField]
    private MeleeCombatInteractor _combatInteractor;

    [SerializeField]
    private List<IEntity> _detectedEntites = new();


    public void OnEntitiesUpdated(List<IEntity> entities)
    {
        _detectedEntites.Clear();
        for (int i = 0, count = entities.Count; i < count; i++)
        {
            var entity = entities[i];
            if (MatchesEntity(entity))
            {
                _detectedEntites.Add(entity);
            }
        }

        OnEntitesChanged(_detectedEntites);
    }
    private bool MatchesEntity(IEntity entity)
    {
        return _enemyCondition.IsTrue(entity);
    }

    private void OnEntitesChanged(List<IEntity> entities)
    {
        if (entities.Count > 0)
        {
            var targetEnemy = entities[0];
            _combatInteractor.TryStartCombat(targetEnemy);
        }
    }
}