using Elementary;
using UnityEngine;


public sealed class ConditionBehaviour_IsMoving : ConditionBehaviour
{
    [SerializeField]
    private MoveInDirectionEngine engine;

    public override bool IsTrue()
    {
        return this.engine.IsMoving;
    }
}