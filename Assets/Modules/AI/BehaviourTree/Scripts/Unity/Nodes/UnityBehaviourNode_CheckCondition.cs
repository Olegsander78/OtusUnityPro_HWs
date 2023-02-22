using UnityEngine;

namespace AI.BTree
{
    [AddComponentMenu(Extensions.MENU_PATH + "Condition Node")]
    public sealed class UnityBehaviourNode_CheckCondition : UnityBehaviourNode
    {
        [SerializeField]
        private bool invertCondition;
        
        [Space]
        [SerializeField]
        private UnityBehaviourCondition[] conditions;

        protected override void Run()
        {
            var isConditionPerforms = this.IsConditionTrue();
            this.Return(isConditionPerforms);
        }

        private bool IsConditionTrue()
        {
            var conditionPerforms = true;
            if (this.conditions != null)
            {
                conditionPerforms = this.CheckConditions();
            }
            
            if (this.invertCondition)
            {
                conditionPerforms = !conditionPerforms;
            }

            return conditionPerforms;
        }

        private bool CheckConditions()
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
}