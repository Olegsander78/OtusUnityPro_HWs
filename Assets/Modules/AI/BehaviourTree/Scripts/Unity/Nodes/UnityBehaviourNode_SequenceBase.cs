namespace AI.BTree
{
    public abstract class UnityBehaviourNode_SequenceBase : UnityBehaviourNode, IBehaviourCallback
    {
        protected abstract UnityBehaviourNode[] Children { get; }

        private int pointer;

        private UnityBehaviourNode currentNode;

        protected override void Run()
        {
            var children = this.Children;
            if (children is not {Length: > 0})
            {
                this.Return(true);
                return;
            }

            this.pointer = 0;
            this.currentNode = children[this.pointer];
            this.currentNode.Run(callback: this);
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            if (!success)
            {
                this.Return(false);
                return;
            }

            var children = this.Children;
            if (this.pointer + 1 >= children.Length)
            {
                this.Return(true);
                return;
            }

            this.pointer++;
            this.currentNode = children[this.pointer];
            this.currentNode.Run(callback: this);
        }

        protected override void OnAbort()
        {
            if (this.currentNode != null && this.currentNode.IsRunning)
            {
                this.currentNode.Abort();
            }
        }
    }
}