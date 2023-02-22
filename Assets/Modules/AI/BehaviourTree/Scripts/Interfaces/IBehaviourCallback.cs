namespace AI.BTree
{
    public interface IBehaviourCallback
    {
        void Invoke(IBehaviourNode node, bool success);
    }
}