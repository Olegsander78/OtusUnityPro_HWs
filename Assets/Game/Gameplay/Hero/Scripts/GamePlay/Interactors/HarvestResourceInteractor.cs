using System;
using System.Collections;
using Entities;
using Game.GameEngine.Mechanics;
using GameElements;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Interactor «Harvest Resource»")]
public sealed class HarvestResourceInteractor : MonoBehaviour, IGameInitElement
{
    [SerializeField]
    private float delay = 0.15f;

    private IComponent_HarvestResource heroComponent;

    private Coroutine delayCoroutine;

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.heroComponent = context
            .GetService<HeroService>()
            .GetHero()
            .Get<IComponent_HarvestResource>();
    }

    public void TryStartHarvest(IEntity resourceObject)
    {
        if (this.heroComponent.IsHarvesting)
        {
            return;
        }

        if (this.delayCoroutine == null)
        {
            this.delayCoroutine = this.StartCoroutine(this.HarvestRoutine(resourceObject));
        }
    }

    private IEnumerator HarvestRoutine(IEntity resourceObject)
    {
        yield return new WaitForSeconds(this.delay);

        var operation = new HarvestResourceOperation(resourceObject);
        if (this.heroComponent.CanStartHarvest(operation))
        {
            this.heroComponent.StartHarvest(operation);
        }
        Debug.Log($"Start Harvest {resourceObject}");
        this.delayCoroutine = null;
    }

    internal bool CanHarvest(IEntity entity)
    {
        return true;
    }

    internal void Harvest(IEntity entity)
    {
        Debug.LogWarning($"Start harvest {entity}");
    }
}