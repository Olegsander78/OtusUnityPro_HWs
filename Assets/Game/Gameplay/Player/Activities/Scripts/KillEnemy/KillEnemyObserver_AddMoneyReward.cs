using Entities;
using GameSystem;
using UnityEngine;


public sealed class KillEnemyObserver_AddMoneyReward : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{
    private HeroService heroService;

    private MoneyStorage moneyStorage;

    //private MoneyPanelAnimator_AddJumpedMoney uiMoneyAnimator;

    private IComponent_MeleeCombat heroComponent;

    [SerializeField]
    private int minMoneyReward = 100;

    [SerializeField]
    private int maxMoneyReward = 300;

    [Space]
    [SerializeField]
    private AudioClip moneySFX;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        this.heroService = context.GetService<HeroService>();
        this.moneyStorage = context.GetService<MoneyStorage>();
        //this.uiMoneyAnimator = context.GetService<MoneyPanelAnimator_AddJumpedMoney>();
    }

    void IGameInitElement.InitGame()
    {
        this.heroComponent = this.heroService.GetHero().Get<IComponent_MeleeCombat>();
    }

    void IGameReadyElement.ReadyGame()
    {
        this.heroComponent.OnCombatStopped += this.OnCombatEnded;
    }

    void IGameFinishElement.FinishGame()
    {
        this.heroComponent.OnCombatStopped -= this.OnCombatEnded;
    }

    private void OnCombatEnded(MeleeCombatOperation operation)
    {
        if (operation.targetDestroyed)
        {
            this.AddMoneyReward(operation.targetEntity);
        }
    }

    private void AddMoneyReward(IEntity targetEnemy)
    {
        var reward = Random.Range(this.minMoneyReward, this.maxMoneyReward + 1);

        //Добавляем монеты в систему
        this.moneyStorage.EarnMoney(reward);

        //Добавляем монеты в UI через партиклы
        var particlePosiiton = targetEnemy.Get<IComponent_GetPosition>().Position;
        //this.uiMoneyAnimator.PlayIncomeFromWorld(particlePosiiton, reward);

        //Звук
        GameAudioManager.PlaySound(GameAudioType.INTERFACE, this.moneySFX);
    }
}