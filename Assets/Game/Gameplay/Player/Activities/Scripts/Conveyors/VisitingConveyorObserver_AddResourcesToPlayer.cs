using System.Collections;
using Entities;
using GameSystem;
using UnityEngine;


public sealed class VisitingConveyorObserver_AddResourcesToPlayer :
    IGameReadyElement,
    IGameFinishElement
{
    private VisitingConveyorService conveyorService;

    private ResourceStorage resourceStorage;

    //private ResourcePanelAnimator_AddResources uiAnimator;

    private MonoBehaviour monContext;

    private IComponent_UnloadZone targetUnloadZone;

    [GameInject]
    public void Construct(
        VisitingConveyorService conveyorService,
        ResourceStorage resourceStorage,
        //ResourcePanelAnimator_AddResources uiAnimator,
        MonoBehaviour monContext
    )
    {
        this.conveyorService = conveyorService;
        this.resourceStorage = resourceStorage;
        //this.uiAnimator = uiAnimator;
        this.monContext = monContext;
    }

    void IGameReadyElement.ReadyGame()
    {
        this.conveyorService.OutputZone.OnEntered += this.OnConveyorEntered;
        this.conveyorService.OutputZone.OnExited += this.OnConveyorExited;
    }

    void IGameFinishElement.FinishGame()
    {
        this.conveyorService.OutputZone.OnEntered -= this.OnConveyorEntered;
        this.conveyorService.OutputZone.OnExited -= this.OnConveyorExited;
    }

    private void OnConveyorEntered(IEntity entity)
    {
        this.targetUnloadZone = entity.Get<IComponent_UnloadZone>();
        this.targetUnloadZone.OnAmountChanged += this.OnAmountChanged;
        this.MoveResourcesFrom(this.targetUnloadZone);
    }

    private void OnConveyorExited(IEntity entity)
    {
        this.targetUnloadZone.OnAmountChanged -= this.OnAmountChanged;
        this.targetUnloadZone = null;
    }

    private void OnAmountChanged(int currentAmount)
    {
        this.monContext.StartCoroutine(this.MoveResourcesInNextFrame(this.targetUnloadZone));
    }

    private IEnumerator MoveResourcesInNextFrame(IComponent_UnloadZone unloadZone)
    {
        yield return new WaitForEndOfFrame();
        this.MoveResourcesFrom(unloadZone);
    }

    private void MoveResourcesFrom(IComponent_UnloadZone unloadZone)
    {
        if (unloadZone.IsEmpty)
        {
            return;
        }

        var resourceType = unloadZone.ResourceType;
        var income = unloadZone.ExtractAll();
        this.resourceStorage.AddResource(resourceType, income);
        //this.uiAnimator.PlayIncomeFromWorld(unloadZone.ParticlePosition, resourceType, income);
    }
}