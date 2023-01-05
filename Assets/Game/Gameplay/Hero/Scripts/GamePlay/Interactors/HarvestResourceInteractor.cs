using System;
using System.Collections.Generic;
using Entities;
using GameElements;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Interactor «Harvest Resource»")]
public sealed class HarvestResourceInteractor : MonoBehaviour,
    IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{
    public event Action<IEntity> OnHarvestCompleted;
    public bool IsHarvesting
    {
        get { return _heroComponent.IsHarvesting; }
    }

    private HeroService _heroService;

    //private ResourceStorage _resourceStorage;

    private IComponent_HarvestResource _heroComponent;    

    [SerializeField]
    private ScriptableEntityCondition _isResourcesActive;

    private readonly List<IHarvestResourceAction> _finishActions = new();

    public void Construct(HeroService heroService)
    {
        _heroService = heroService;
    }
    public void RegisterFinishAction(IHarvestResourceAction action)
    {
        _finishActions.Add(action);
    }
    void IGameInitElement.InitGame(IGameContext context)
    {
        _heroComponent = context
            .GetService<HeroService>()
            .GetHero()
            .Get<IComponent_HarvestResource>();

        _finishActions.Add(new HarvestResourceAction_DestroyResource());
        _finishActions.Add(new HarvestResourceAction_AddResourcesToStorage(
            context.GetService<ResourceStorage>()));

        //_resourceStorage = context .GetService<ResourceStorage>();
    }
    void IGameReadyElement.ReadyGame(IGameContext context)
    {
        _heroComponent.OnHarvestStopped += OnHarvestFinished;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _heroComponent.OnHarvestStopped -= OnHarvestFinished;
    }

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


    //    var operation = new HarvestResourceOperation(resourceObject);
    //    if (this.heroComponent.CanStartHarvest(operation))
    //    {
    //        this.heroComponent.StartHarvest(operation);
    //    }
    //    Debug.Log($"Start Harvest {resourceObject}");
    //    this.delayCoroutine = null;
    //}
    
    internal bool CanHarvest(IEntity resource)
    {
        if (IsHarvesting)
            return false;
        
        if (!_isResourcesActive.IsTrue(resource))
            return false;

        var operation = new HarvestResourceOperation(resource);
        if (!_heroComponent.CanStartHarvest(operation))
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

        var operation = new HarvestResourceOperation(resource);
        _heroComponent.StartHarvest(operation);

        Debug.LogWarning($"Start harvest {resource}");
    }
    public void CancelHarvest()
    {
        if (IsHarvesting)
        {
            _heroComponent.StopHarvest();
            Debug.Log("Interactor: Cancel harvest resource");
        }
    }

    private void OnHarvestFinished(HarvestResourceOperation operation)
    {
        for (int i = 0, count = _finishActions.Count; i < count; i++)
        {
            var action = _finishActions[i];
            action.Do(operation);
        }

        //if (operation.IsCompleted)
        //{
        //    var resource = operation.TargetResource;
        //    DestroyResource(resource);
        //    AddResourcesToStorage(resource);

        //    Debug.LogWarning($" Interactor: Completed harvest {resource}");
        //}
    }    

    //private void DestroyResource(IEntity resource)
    //{
    //    resource.Get<IComponent_Collect>().Collect();
    //}
    //private void AddResourcesToStorage(IEntity resource)
    //{
    //    var resourceType = resource.Get<IComponent_GetResourceType>().ResourceType;
    //    var resourceAmount = resource.Get<IComponent_GetResourceCount>().ResourceCount;
    //    _resourceStorage.AddResource(resourceType, resourceAmount);        
    //}    
}