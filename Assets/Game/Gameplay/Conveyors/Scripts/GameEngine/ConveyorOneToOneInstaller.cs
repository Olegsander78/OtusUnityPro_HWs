using System;
using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ConveyorOneToOneInstaller : MonoBehaviour
{
    [SerializeField]
    private ScriptableConveyourOneToOne conveyourInfo;

    [SerializeField]
    private ResourceInfoCatalog resourceCatalog;

    [Header("Components")]
    [SerializeField]
    private Component_ConveyorId idComponent;

    [SerializeField]
    private Component_LoadZone loadComponent;

    [SerializeField]
    private Component_UnloadZone unloadComponent;

    [Header("Mechanics")]
    [SerializeField]
    private LimitedIntBehavior inputStorage;

    [SerializeField]
    private LimitedIntBehavior outputStorage;

    [SerializeField]
    private TimerBehaviour workTimer;

    [Header("UI")]
    [SerializeField]
    private InfoWidget widget;

    private void Awake()
    {
        this.Install();
    }

    [Button]
    private void Install()
    {
        var inputType = this.conveyourInfo.inputResourceType;
        var outputType = this.conveyourInfo.outputResourceType;

        this.loadComponent.ResourceType = inputType;
        this.unloadComponent.ResourceType = outputType;
        this.idComponent.Id = this.conveyourInfo.id;

        this.inputStorage.MaxValue = this.conveyourInfo.inputCapacity;
        this.outputStorage.MaxValue = this.conveyourInfo.outputCapacity;
        this.workTimer.Duration = this.conveyourInfo.workTime;

        var inputIcon = this.resourceCatalog.FindResource(inputType).icon;
        var outputIcon = this.resourceCatalog.FindResource(outputType).icon;
        this.widget.SetInputIcon(inputIcon);
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