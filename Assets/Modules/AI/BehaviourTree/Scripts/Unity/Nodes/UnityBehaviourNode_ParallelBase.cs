namespace AI.BTree
{
    public abstract class UnityBehaviourNode_ParallelBase : UnityBehaviourNode, IBehaviourCallback
    {
        protected abstract UnityBehaviourNode[] Children { get; }

        protected override void Run()
        {
            var children = this.Children;
            if (children == null)
            {
                return;
            }

            for (int i = 0, count = children.Length; i < count; i++)
            {
                var child = children[i];
                child.Run(callback: this);
            }
        }

        protected override void OnAbort()
        {
            var children = this.Children;
            if (children == null)
            {
                return;
            }

            for (int i = 0, count = children.Length; i < count; i++)
            {
                var child = children[i];
                if (child.IsRunning)
                {
                    child.Abort();
                }
            }
        }

        public virtual void Invoke(IBehaviourNode node, bool success)
        {
        }
    }
}