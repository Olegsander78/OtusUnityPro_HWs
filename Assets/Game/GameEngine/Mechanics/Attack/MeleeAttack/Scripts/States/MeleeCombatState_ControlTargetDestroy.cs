using Elementary;
using UnityEngine;


public sealed class MeleeCombatState_ControlTargetDestroy : MonoState
{
    [Space]
    [SerializeField]
    private MeleeCombatEngine combatEngine;

    [SerializeField]
    private Object attacker;

    private IComponent_OnDestroyed targetComponent;

    public override void Enter()
    {
        this.targetComponent = this.combatEngine.CurrentOperation.targetEntity.Get<IComponent_OnDestroyed>();
        this.targetComponent.OnDestroyed += this.OnTargetDestroyed;
    }

    public override void Exit()
    {
        this.targetComponent.OnDestroyed -= this.OnTargetDestroyed;
    }

    private void OnTargetDestroyed(DestroyEvent destroyEvent)
    {
        if (this.IsDestroyedByMe(destroyEvent))
        {
            this.combatEngine.CurrentOperation.targetDestroyed = true;
        }

        this.combatEngine.StopCombat();
    }

    private bool IsDestroyedByMe(DestroyEvent destroyEvent)
    {
        return destroyEvent.reason == DestroyReason.ATTACKER &&
               ReferenceEquals(destroyEvent.source, this.attacker);
    }
}