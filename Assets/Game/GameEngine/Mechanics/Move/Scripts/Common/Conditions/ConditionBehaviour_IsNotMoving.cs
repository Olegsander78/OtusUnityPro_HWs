using Elementary;
using UnityEngine;


public sealed class ConditionBehaviour_IsNotMoving : ConditionBehaviour
{
    [SerializeField]
    private MoveInDirectionEngine engine;

    public override bool IsTrue()
    {
        return !this.engine.IsMoving;
    }
}