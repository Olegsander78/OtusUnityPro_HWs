using Entities;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ConveyorTwoToOneTest : MonoBehaviour
{
    [SerializeField]
    private UnityEntity _conveyor;

    [SerializeField]
    private Component_LoadZone _component_LoadZoneOne;

    [SerializeField]
    private Component_LoadZone _component_LoadZoneTwo;

    [SerializeField]
    private DoubleWorkOnLoadEngine _engine;

    [Button]
    private void LoadResources(int resourceOneCount, int resourceTwoCount)
    {        
        if (!_component_LoadZoneOne.IsFull)
        {
            _component_LoadZoneOne.PutAmount(resourceOneCount);
        }
        if (!_component_LoadZoneTwo.IsFull)
        {
            _component_LoadZoneTwo.PutAmount(resourceTwoCount);
        }
    }

    [Button]
    private void TakeAllResources()
    {
        var unloadComponent = _conveyor.Get<IComponent_UnloadZone>();
        
        if (!unloadComponent.IsEmpty)
        {
            var extractedResources = unloadComponent.ExtractAll();
            Debug.Log($"Extracted resources {extractedResources} {_engine.ItemOnUnload.Name}");
        }
    }
}