using System.Collections;
using Entities;
using Game.GameEngine.Mechanics;
using GameElements;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Interactor «Harvest Resource»")]
public sealed class HarvestResourceInteractor : MonoBehaviour, IGameInitElement
{
    //[SerializeField]
    //private float delay = 0.15f;

    //private IComponent_HarvestResource heroComponent;

    //private Coroutine delayCoroutine;

    //public void TryStartHarvest(IEntity resourceObject)
    //{
    //    if (this.heroComponent.IsHarvesting)
    //    {
    //        return;
    //    }

    //    if (this.delayCoroutine == null)
    //    {
    //        this.delayCoroutine = this.StartCoroutine(this.HarvestRoutine(resourceObject));
    //    }
    //}

    //private IEnumerator HarvestRoutine(IEntity resourceObject)
    //{
    //    yield return new WaitForSeconds(this.delay);

    //    var operation = new HarvestResourceOperation(resourceObject);
    //    if (this.heroComponent.CanStartHarvest(operation))
    //    {
    //        this.heroComponent.StartHarvest(operation);
    //    }

    //    this.delayCoroutine = null;
    //}

    void IGameInitElement.InitGame(IGameContext context)
    {
    //    this.heroComponent = context
    //        .GetService<HeroService>()
    //        .GetHero()
    //        .Get<IComponent_HarvestResource>();
    }
}