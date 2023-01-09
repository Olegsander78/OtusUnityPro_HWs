using Elementary;
using UnityEngine;


public sealed class ConditionBehaviour_IsNotMeleeCombat : ConditionBehaviour
{
    [SerializeField]
    private MeleeCombatEngine engine;

    public override bool IsTrue()
    {
        return !this.engine.IsCombat;
    }
}