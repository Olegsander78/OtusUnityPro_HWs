using Elementary;
using UnityEngine;


public sealed class ConditionBehaviour_IsHitPointsFull : ConditionBehaviour
{
    [SerializeField]
    private HitPointsEngine engine;

    public override bool IsTrue()
    {
        return this.engine.CurrentHitPoints >= this.engine.MaxHitPoints;
    }
}