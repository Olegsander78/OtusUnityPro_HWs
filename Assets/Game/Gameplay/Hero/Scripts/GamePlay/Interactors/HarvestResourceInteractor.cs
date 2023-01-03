using System;
using System.Collections;
using Entities;
using Game.GameEngine.Mechanics;
using GameElements;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Interactor «Harvest Resource»")]
public sealed class HarvestResourceInteractor : MonoBehaviour, IGameInitElement
{
    public event Action<IEntity> OnHarvestCompleted;
    public bool IsHarvesting { get; private set; }

    [SerializeField]
    private float delay = 0.15f;

    [SerializeField]
    private float _duration = 5f;

    private IEntity _currentResource;

    private ResourceStorage _resourceStorage;

    private IComponent_HarvestResource heroComponent;

    private Coroutine delayCoroutine;

    private Coroutine _harvestCoroutine;

    [SerializeField]
    private ScriptableEntityCondition _isResourcesActive;    

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.heroComponent = context
            .GetService<HeroService>()
            .GetHero()
            .Get<IComponent_HarvestResource>();

        _resourceStorage = context .GetService<ResourceStorage>();
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
        if (!CanHarvest(resource))
        {
            Debug.LogWarning($"Can't harvest {resource}");
            return;
        }

        Debug.LogWarning($"Start harvest {resource}");

        IsHarvesting= true;
        _currentResource = resource;
        _harvestCoroutine = StartCoroutine(StartHarvestRoutine());
    }

    private IEnumerator StartHarvestRoutine()
    {
        yield return new WaitForSeconds(_duration);

        var resource = _currentResource;
        DestroyResource(resource);
        AddResourcesToStorage(resource);
        ResetState();

        Debug.LogWarning($"Completed harvest {resource}");
        OnHarvestCompleted?.Invoke(resource);
    }

    private void ResetState()
    {
        _currentResource= null;
        IsHarvesting = false;
        _harvestCoroutine= null;
    }

    private void DestroyResource(IEntity resource)
    {
        resource.Get<IComponent_Collect>().Collect();
    }
    private void AddResourcesToStorage(IEntity resource)
    {
        var resourceType = resource.Get<IComponent_GetResourceType>().ResourceType;
        var resourceAmount = resource.Get<IComponent_GetResourceCount>().ResourceCount;
        _resourceStorage.AddResource(resourceType, resourceAmount);
    }

    internal void CancelHarvest()
    {
        if (IsHarvesting)
        {
            StopCoroutine(_harvestCoroutine);
            ResetState();
            Debug.Log("Cancel harvest resource");
        }            
    }
}