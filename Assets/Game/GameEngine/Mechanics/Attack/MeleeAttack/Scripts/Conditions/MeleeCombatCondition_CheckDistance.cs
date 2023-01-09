using Elementary;
using UnityEngine;


public sealed class MeleeCombatCondition_CheckDistance : MeleeCombatCondition
{
    [SerializeField]
    private TransformEngine myTransform;

    [SerializeField]
    private FloatAdapter minDistance;

    public override bool IsTrue(MeleeCombatOperation value)
    {
        var targetPosition = value.targetEntity.Get<IComponent_GetPosition>().Position;
        return this.myTransform.IsDistanceReached(targetPosition, this.minDistance.Value);
    }
}