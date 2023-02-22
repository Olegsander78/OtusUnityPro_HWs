using System;
using UnityEngine;

namespace AI.BTree
{
    [Serializable]
    public sealed class BehaviourCondition_Invert : IBehaviourCondition
    {
        [SerializeReference]
        public IBehaviourCondition condition;

        public BehaviourCondition_Invert(IBehaviourCondition condition)
        {
            this.condition = condition;
        }

        public BehaviourCondition_Invert()
        {
        }

        public bool IsTrue()
        {
            return !this.condition.IsTrue();
        }
    }
}