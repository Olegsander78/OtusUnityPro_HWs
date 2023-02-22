using System;
using UnityEngine;

namespace AI.BTree
{
    [Serializable]
    public class BehaviourTree : BehaviourNode, IBehaviourTree, IBehaviourCallback
    {
        public event Action OnStarted;
        
        public event Action<bool> OnFinished;

        public event Action OnAborted;

        [SerializeReference]
        public IBehaviourNode root = default;

        public BehaviourTree(IBehaviourNode root)
        {
            this.root = root;
        }

        public BehaviourTree()
        {
        }

        protected override void Run()
        {
            if (!this.root.IsRunning)
            {
                this.OnStarted?.Invoke();
                this.root.Run(callback: this);
            }
        }

        protected override void OnAbort()
        {
            if (this.root.IsRunning)
            {
                this.root.Abort();
                this.OnAborted?.Invoke();
            }
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            this.Return(success);
            this.OnFinished?.Invoke(success);
        }
    }
}