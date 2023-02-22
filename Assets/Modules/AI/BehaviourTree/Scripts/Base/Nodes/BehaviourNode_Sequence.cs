using System;
using UnityEngine;

namespace AI.BTree
{
    [Serializable]
    public sealed class BehaviourNode_Sequence : BehaviourNode, IBehaviourCallback
    {
        [SerializeReference]
        public IBehaviourNode[] children;

        private IBehaviourNode currentNode;

        private int pointer;

        public BehaviourNode_Sequence(params IBehaviourNode[] children)
        {
            this.children = children;
        }

        public BehaviourNode_Sequence()
        {
        }

        protected override void Run()
        {
            if (this.children.Length <= 0)
            {
                this.Return(true);
                return;
            }

            this.pointer = 0;
            this.currentNode = this.children[this.pointer];
            this.currentNode.Run(callback: this);
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            if (!success)
            {
                this.Return(false);
                return;
            }

            if (this.pointer + 1 >= this.children.Length)
            {
                this.Return(true);
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