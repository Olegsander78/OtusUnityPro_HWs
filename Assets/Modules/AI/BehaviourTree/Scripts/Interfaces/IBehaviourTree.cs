using System;

namespace AI.BTree
{
    public interface IBehaviourTree : IBehaviourNode
    {
        event Action OnStarted;
    
        event Action<bool> OnFinished;

        event Action OnAborted;
    }
}