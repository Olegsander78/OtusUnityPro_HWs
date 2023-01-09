using Elementary;
using UnityEngine;


public sealed class MeleeCombatCondition_BaseConditions : MeleeCombatCondition
{
    [SerializeField]
    private ConditionBehaviour[] conditions;

    public override bool IsTrue(MeleeCombatOperation operation)
    {
        for (int i = 0, count = this.conditions.Length; i < count; i++)
        {
            var condition = this.conditions[i];
            if (!condition.IsTrue())
            {
                return false;
            }
        }

        return true;
    }
}