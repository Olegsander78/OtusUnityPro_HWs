using System;
using UnityEngine;


[Serializable]
public sealed class HarvestResourceAction_RotateToResource : IHarvestResourceAction
{
    [SerializeField]
    private TransformEngine transformEngine;

    public void Do(HarvestResourceOperation operation)
    {
        var target = operation.TargetResource.Get<IComponent_GetPosition>().Position;
        this.transformEngine.LookAtPosition(target);
        Debug.Log("Look at resource!");
    }
}