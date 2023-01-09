using Elementary;
using UnityEngine;


public sealed class ConditionBehaviour_IsHitPointsExists : ConditionBehaviour
{
    [SerializeField]
    private HitPointsEngine engine;

    public override bool IsTrue()
    {
        return this.engine.CurrentHitPoints > 0;
    }
}