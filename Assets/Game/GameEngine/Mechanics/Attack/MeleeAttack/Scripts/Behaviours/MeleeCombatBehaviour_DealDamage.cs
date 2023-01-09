using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class MeleeCombatBehaviour_DealDamage : MonoBehaviour
{
    [SerializeField]
    private MeleeCombatEngine combatEngine;

    [SerializeField]
    private Object attacker;

    [SerializeField]
    private IntAdapter damage;

    [Title("Methods")]
    [Button]
    [GUIColor(0, 1, 0)]
    public void DealDamage()
    {
        if (!this.combatEngine.IsCombat)
        {
            return;
        }

        var target = this.combatEngine.CurrentOperation.targetEntity;
        var aliveComponent = target.Get<IComponent_IsAlive>();
        if (!aliveComponent.IsAlive)
        {
            return;
        }

        var takeDamageComponent = target.Get<IComponent_TakeDamage>();
        var damageEvent = new TakeDamageEvent(
            this.damage.Value,
            TakeDamageReason.MELEE,
            this.attacker
        );
        takeDamageComponent.TakeDamage(damageEvent);
    }
}