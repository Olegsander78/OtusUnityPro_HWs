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

    [SerializeField]
    private float _duration = 5f;

    private IEntity _currentResource;

    private ResourceStorage _resourceStorage;

    private IComponent_HarvestResource heroComponent;

    private Coroutine delayCoroutine;

    [SerializeField]
    private ScriptableEntityCondition _isResourcesActive;
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

    internal bool CanHarvest(IEntity resource)
    {
        if (!_isResourcesActive.IsTrue(resource))
            return false;

        return true;
    }

    internal void StartHarvest(IEntity resource)
    {
        Debug.LogWarning($"Start harvest {resource}");

        _currentResource = resource;
        StartCoroutine(StartTimerRoutine());
    }

    private IEnumerator StartTimerRoutine()
    {
        yield return new WaitForSeconds(_duration);
        DestroyResource();
    }
    private void DestroyResource()
    {
        _currentResource.Get<IComponent_Collect>().Collect();
    }
    private void AddResources()
    {
        var resourceType = _currentResource.Get<IComponent_GetResourceType>().ResourceType;
        var resourceAmount = _currentResource.Get<IComponent_GetResourceCount>().ResourceCount;
        _resourceStorage.AddResource(resourceType, resourceAmount);
    }
}