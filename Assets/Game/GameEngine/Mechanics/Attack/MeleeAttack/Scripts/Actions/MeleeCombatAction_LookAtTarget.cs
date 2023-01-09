using UnityEngine;


public sealed class MeleeCombatAction_LookAtTarget : MeleeCombatAction
{
    [SerializeField]
    private TransformEngine lookAtScript;

    public override void Do(MeleeCombatOperation operation)
    {
        var targetPosition = operation.targetEntity.Get<IComponent_GetPosition>().Position;
        this.lookAtScript.LookAtPosition(targetPosition);
    }
}