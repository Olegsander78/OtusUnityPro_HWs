using UnityEngine;


public sealed class MeleeCombatState_ControlTargetDistance : State_CheckDistanceToTarget
{
    [Space]
    [SerializeField]
    private MeleeCombatEngine combatEngine;

    private IComponent_GetPosition targetComponent;

    public override void Enter()
    {
        this.targetComponent = this.combatEngine.CurrentOperation.targetEntity.Get<IComponent_GetPosition>();
        base.Enter();
    }

    protected override void OnUpdate(bool distanceReached)
    {
        if (!distanceReached)
        {
            this.combatEngine.StopCombat();
        }
    }

    protected override Vector3 GetTargetPosition()
    {
        return this.targetComponent.Position;
    }
}