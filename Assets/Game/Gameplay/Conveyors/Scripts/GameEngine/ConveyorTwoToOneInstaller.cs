using System;
using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ConveyorTwoToOneInstaller : MonoBehaviour
{
    [SerializeField]
    private ScriptableConveyourTwoToOne conveyourInfo;

    [SerializeField]
    private ResourceInfoCatalog resourceCatalog;

    [SerializeField]
    private ItemInfoCatalog ItemCatalog;

    [Header("Components")]
    [SerializeField]
    private Component_ConveyorId idComponent;

    [SerializeField]
    private Component_LoadZone loadOneComponent;

    [SerializeField]
    private Component_LoadZone loadTwoComponent;

    [SerializeField]
    private Component_ItemUnloadZone unloadComponent;

    [Header("Mechanics")]
    [SerializeField]
    private LimitedIntBehavior inputOneStorage;

    [SerializeField]
    private LimitedIntBehavior inputTwoStorage;

    [SerializeField]
    private LimitedIntBehavior outputStorage;

    //[SerializeField]
    //private ItemFromTwoResourcesInfo itemFromTwoResources;

    [SerializeField]
    private TimerBehaviour workTimer;

    [Header("UI")]
    [SerializeField]
    private InfoWidgetTwoInput widget;

    private void Awake()
    {
        this.Install();
    }

    [Button]
    private void Install()
    {
        var inputOneType = this.conveyourInfo.inputOneResourceType;
        var inputTwoType = this.conveyourInfo.inputTwoResourceType;
        var outputType = this.conveyourInfo.outputItem;

        this.loadOneComponent.ResourceType = inputOneType;
        this.loadTwoComponent.ResourceType = inputTwoType;
        this.unloadComponent.ItemFromTwoResources = outputType;
        this.idComponent.Id = this.conveyourInfo.id;

        this.inputOneStorage.MaxValue = this.conveyourInfo.inputOneCapacity;
        this.inputTwoStorage.MaxValue = this.conveyourInfo.inputTwoCapacity;
        this.outputStorage.MaxValue = this.conveyourInfo.outputCapacity;
        this.workTimer.Duration = this.conveyourInfo.workTime;

        var inputOneIcon = this.resourceCatalog.FindResource(inputOneType).icon;
        var inputTwoIcon = this.resourceCatalog.FindResource(inputTwoType).icon;
        var outputIcon = this.ItemCatalog.FindItems(outputType.type).icon;
        this.widget.SetInputIcon(inputOneIcon,inputTwoIcon);
        this.widget.SetOutputIcon(outputIcon);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        try
        {
            this.Install();
        }
        catch (Exception)
        {
        }
    }
#endif
}