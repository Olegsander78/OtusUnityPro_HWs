using System;
using System.Collections;
using Entities;
using GameSystem;
using UnityEngine;


public sealed class VisitingConveyorObserver_ExtractPlayerResources :
    IGameReadyElement,
    IGameFinishElement
{
    private VisitingConveyorService conveyorService;

    private ResourceStorage resourceStorage;

    private MonoBehaviour monoContext;

    private IComponent_LoadZone targetLoadZone;

    [GameInject]
    public void Construct(
        VisitingConveyorService conveyorService,
        ResourceStorage resourceStorage,
        MonoBehaviour monoContext
    )
    {
        this.conveyorService = conveyorService;
        this.resourceStorage = resourceStorage;
        this.monoContext = monoContext;
    }

    void IGameReadyElement.ReadyGame()
    {
        this.conveyorService.InputZone.OnEntered += this.OnConveyorEntered;
        this.conveyorService.InputZone.OnExited += this.OnConveyorExited;
    }

    void IGameFinishElement.FinishGame()
    {
        this.conveyorService.InputZone.OnEntered -= this.OnConveyorEntered;
        this.conveyorService.InputZone.OnExited -= this.OnConveyorExited;
    }

    private void OnConveyorEntered(IEntity entity)
    {
        this.targetLoadZone = entity.Get<IComponent_LoadZone>();
        this.targetLoadZone.OnAmountChanged += this.OnAmountChanged;
        this.MoveResourcesTo(this.targetLoadZone);
    }

    private void OnConveyorExited(IEntity entity)
    {
        this.targetLoadZone.OnAmountChanged -= this.OnAmountChanged;
        this.targetLoadZone = null;
    }

    private void OnAmountChanged(int currentAmount)
    {
        this.monoContext.StartCoroutine(this.MoveResourcesInNextFrame(this.targetLoadZone));
    }

    private IEnumerator MoveResourcesInNextFrame(IComponent_LoadZone loadZone)
    {
        yield return new WaitForEndOfFrame();
        this.MoveResourcesTo(loadZone);
    }

    private void MoveResourcesTo(IComponent_LoadZone loadZone)
    {
        if (loadZone.IsFull)
        {
            return;
        }

        var resourceType = loadZone.ResourceType;
        var resourcesInStorage = this.resourceStorage.GetResource(resourceType);
        if (resourcesInStorage <= 0)
        {
            return;
        }

        var resultAmount = Math.Min(resourcesInStorage, loadZone.AvailableAmount);
        this.resourceStorage.ExtractResource(resourceType, resultAmount);
        loadZone.PutAmount(resultAmount);
    }
}