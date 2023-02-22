using System;
using UnityEngine;

namespace AI.BTree
{
    [Serializable]
    public sealed class BehaviourNode_Selector : BehaviourNode, IBehaviourCallback
    {
        [SerializeReference]
        public IBehaviourNode[] children;

        private IBehaviourNode currentNode;

        private int pointer;

        public BehaviourNode_Selector(params IBehaviourNode[] children)
        {
            this.children = children;
        }

        public BehaviourNode_Selector()
        {
        }

        protected override void Run()
        {
            if (this.children == null && this.children.Length <= 0)
            {
                this.Return(false);
                return;
            }

            this.pointer = 0;

            this.currentNode = this.children[this.pointer];
            this.currentNode.Run(callback: this);
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            if (success)
            {
                this.Return(true);
                return;
            }

            if (this.pointer + 1 >= this.children.Length)
            {
                this.Return(false);
                return;
            }

            this.pointer++;
            this.currentNode = this.children[this.pointer];
            this.currentNode.Run(callback: this);
        }

        protected override void OnAbort()
        {
            if (this.currentNode is {IsRunning: true})
            {
                this.currentNode.Abort();
            }
        }
    }
}