using GameElements;
using Entities;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemiesLevelController : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    private IEntity _hero;

    private IComponent_GetLevel _component_GetLevelHero;

    private IComponent_OnLevelChanged _component_OnLevelHeroChanged;    
    
    private int _levelMultiplier;

    [Header("Stats Enemy")]
    [ReadOnly]
    [ShowInInspector]
    private int _baseHp;

    [SerializeField]
    private int _stepHpOnLevel;

    [SerializeField]
    private HitPointsEngine _hitPointsEngine;


    void IGameInitElement.InitGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();
        _component_GetLevelHero = _hero.Get<IComponent_GetLevel>();
        _component_OnLevelHeroChanged = _hero.Get<IComponent_OnLevelChanged>();

        _levelMultiplier = _component_GetLevelHero.Level;

        UpdateEnemyStats(_levelMultiplier);
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _component_OnLevelHeroChanged.OnLevelChanged += UpdateEnemyStats;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _component_OnLevelHeroChanged.OnLevelChanged -= UpdateEnemyStats;
    }

    private void UpdateEnemyStats(int level)
    {
        _levelMultiplier = level;
        _baseHp = _hitPointsEngine.CurrentHitPoints;

        _hitPointsEngine.MaxHitPoints = _baseHp + (_levelMultiplier - 1) * _stepHpOnLevel;
        _hitPointsEngine.CurrentHitPoints = _hitPointsEngine.CurrentHitPoints;
    }
}
