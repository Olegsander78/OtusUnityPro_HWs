using System;
using UnityEngine;

namespace AI.BTree
{
    [Serializable]
    public sealed class BehaviourNode_Decorator : BehaviourNode, IBehaviourCallback
    {
        [SerializeReference]
        public IBehaviourNode node = default;

        [SerializeField]
        public bool success = true;

        public BehaviourNode_Decorator()
        {
        }

        public BehaviourNode_Decorator(IBehaviourNode node, bool success)
        {
            this.node = node;
            this.success = success;
        }

        protected override void Run()
        {
            this.node.Run(callback: this);
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            this.Return(this.success);
        }

        protected override void OnAbort()
        {
            if (this.node.IsRunning)
            {
                this.node.Abort();
            }
        }
    }
}