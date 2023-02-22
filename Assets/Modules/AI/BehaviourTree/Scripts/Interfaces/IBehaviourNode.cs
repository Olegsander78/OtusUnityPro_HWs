namespace AI.BTree
{
    public interface IBehaviourNode
    {
        public bool IsRunning { get; }

        public void Run(IBehaviourCallback callback = null);

        public void Abort();
    }
}