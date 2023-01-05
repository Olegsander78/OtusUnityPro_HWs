using System;
using UnityEngine;


[Serializable]
public sealed class HarvestResourceCondition_CheckDistanceToResource : IHarvestResourceCondition
{
    [SerializeField]
    private TransformEngine transform;

    [SerializeField]
    private float minDistance = 1.25f;

    public bool IsTrue(HarvestResourceOperation operation)
    {
        Vector3 targetPosition = operation.TargetResource.Get<IComponent_GetPosition>().Position;
        Vector3 myPosition = this.transform.WorldPosition;
        var distanceVector = targetPosition - myPosition;
        var isTrue = distanceVector.magnitude <= this.minDistance;
        Debug.Log($"CHECK CONDITION {isTrue} {distanceVector.magnitude}");
        return isTrue;
    }
}