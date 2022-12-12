using Entities;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ConveyorOneToOneTest : MonoBehaviour
{
    [SerializeField]
    private UnityEntity conveyor;

    [Button]
    private void LoadResources(int resourceCount)
    {
        var loadComponent = this.conveyor.Get<IComponent_LoadZone>();
        if (!loadComponent.IsFull)
        {
            loadComponent.PutAmount(resourceCount);
        }
    }

    [Button]
    private void TakeAllResources()
    {
        var unloadComponent = this.conveyor.Get<IComponent_UnloadZone>();
        if (!unloadComponent.IsEmpty)
        {
            var extractedResources = unloadComponent.ExtractAll();
            Debug.Log($"Extracted resources {extractedResources}");
        }
    }
}