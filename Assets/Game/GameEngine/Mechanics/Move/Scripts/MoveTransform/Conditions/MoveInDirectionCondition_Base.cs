using Elementary;
using UnityEngine;


public sealed class MoveInDirectionCondition_Base : MoveInDirecitonCondition
{
    [SerializeField]
    private ConditionBehaviour[] conditions;

    public override bool IsTrue(Vector3 value)
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