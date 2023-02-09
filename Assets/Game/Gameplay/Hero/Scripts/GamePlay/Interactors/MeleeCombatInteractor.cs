using System;
using System.Collections;
using Entities;
using GameSystem;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Interactor «Melee Combat»")]
public sealed class MeleeCombatInteractor : MonoBehaviour,
    IGameConstructElement
{
    [SerializeField]
    private float delay = 0.15f;

    private IComponent_MeleeCombat heroComponent;

    private Coroutine delayCoroutine;

    public void ConstructGame(IGameContext context)
    {
        this.heroComponent = context.GetService<HeroService>().GetHero().Get<IComponent_MeleeCombat>();
    }    
    public void TryStartCombat(IEntity target)
    {
        if (this.heroComponent.IsCombat)
        {
            return;
        }

        if (this.delayCoroutine == null)
        {
            this.delayCoroutine = this.StartCoroutine(this.CombatRoutine(target));
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