using GameSystem;
using Entities;
using UnityEngine;

public class EnemiesRewardsController : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    private IEntity _hero;

    private IComponent_GetLevel _component_GetLevelHero;

    private IComponent_OnLevelChanged _component_OnLevelHeroChanged;

    private int _levelMultiplier;

    [Header("Rewards for Enemy")]
    [SerializeField]
    private UnityEntity _entity;

    [SerializeField]
    private float _rateExp;

    [SerializeField]
    private float _rateMoney;

    private int _baseExp;

    private int _baseMoney;

    public virtual void ConstructGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();
    }

    void IGameInitElement.InitGame()
    {       
        _component_GetLevelHero = _hero.Get<IComponent_GetLevel>();
        _component_OnLevelHeroChanged = _hero.Get<IComponent_OnLevelChanged>();

        _levelMultiplier = _component_GetLevelHero.Level;

        _baseExp = _entity.Get<IComponent_ExpRewarded>().ExpReward;
        _baseMoney = _entity.Get<IComponent_MoneyRewarded>().MoneyReward;
        //Debug.Log($"Base rewards: {_baseExp} , {_baseMoney} ");

        UpdateRewards(_levelMultiplier);
        //Debug.Log($"Init rewards: {_baseExp} , {_baseMoney} ");
    }

    void IGameStartElement.StartGame()
    {
        _component_OnLevelHeroChanged.OnLevelChanged += UpdateRewards;
    }

    void IGameFinishElement.FinishGame()
    {
        _component_OnLevelHeroChanged.OnLevelChanged -= UpdateRewards;
    }

    private void UpdateRewards(int level)
    {
        _levelMultiplier = level;

        if (_entity != null)
        {
            _entity.Get<IComponent_ExpRewarded>().ExpReward = Mathf.RoundToInt(_baseExp * _levelMultiplier * _rateExp);
            _entity.Get<IComponent_MoneyRewarded>().MoneyReward = Mathf.RoundToInt(_baseMoney * _levelMultiplier * _rateMoney);
            //Debug.Log($"OnUpgrade Rewards {_entity.Get<IComponent_GetEnemyInfo>().EnemyInfo.EnemyType}: Exp - {_entity.Get<IComponent_ExpRewarded>().ExpReward} , Money - { _entity.Get<IComponent_MoneyRewarded>().MoneyReward}");
        }
    }
}
