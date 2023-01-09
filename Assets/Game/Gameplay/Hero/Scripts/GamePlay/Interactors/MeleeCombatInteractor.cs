using System;
using System.Collections;
using Entities;
using GameElements;
using UnityEngine;


[Serializable]
public sealed class MeleeCombatInteractor : MonoBehaviour,
    IGameInitElement
{
    //private HeroService heroService;

    private MonoBehaviour monoContext;

    [SerializeField]
    private float delay = 0.15f;

    private IComponent_MeleeCombat heroComponent;

    private Coroutine delayCoroutine;

    public void InitGame(IGameContext context)
    {
        this.heroComponent = context.GetService<HeroService>().GetHero().Get<IComponent_MeleeCombat>();
    }

    //[GameInject]
    //public void Construct(HeroService heroService, MonoBehaviour monoContext)
    //{
    //    this.heroService = heroService;
    //    this.monoContext = monoContext;
    //}
    //void IGameInitElement.InitGame()
    //{
    //    this.heroComponent = this.heroService.GetHero().Get<IComponent_MeleeCombat>();
    //}


    public void TryStartCombat(IEntity target)
    {
        if (this.heroComponent.IsCombat)
        {
            return;
        }

        if (this.delayCoroutine == null)
        {
            this.delayCoroutine = this.monoContext.StartCoroutine(this.CombatRoutine(target));
        }
    }

    private IEnumerator CombatRoutine(IEntity target)
    {
        yield return new WaitForSeconds(this.delay);

        var operation = new MeleeCombatOperation(target);
        if (this.heroComponent.CanStartCombat(operation))
        {
            this.heroComponent.StartCombat(operation);
        }

        this.delayCoroutine = null;
    }
}