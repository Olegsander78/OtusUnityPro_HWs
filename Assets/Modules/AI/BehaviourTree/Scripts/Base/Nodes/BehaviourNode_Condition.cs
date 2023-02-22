using System;
using UnityEngine;

namespace AI.BTree
{
    [Serializable]
    public sealed class BehaviourNode_Condition : BehaviourNode
    {
        [Space]
        [SerializeReference]
        public IBehaviourCondition condition;

        public BehaviourNode_Condition()
        {
        }

        public BehaviourNode_Condition(IBehaviourCondition condition)
        {
            this.condition = condition;
        }

        protected override void Run()
        {
            this.Return(this.condition.IsTrue());
        }
    }
}