using Entities;
using UnityEngine;


public sealed class MeleeCombatCondition_CheckEntity : MeleeCombatCondition
{
    [SerializeField]
    private ScriptableEntityCondition[] conditions;

    public override bool IsTrue(MeleeCombatOperation operation)
    {
        var targetEntity = operation.targetEntity;
        for (int i = 0, count = this.conditions.Length; i < count; i++)
        {
            var condition = this.conditions[i];
            if (!condition.IsTrue(targetEntity))
            {
                return false;
            }
        }

        return true;
    }
}