using UnityEngine;

namespace AI.BTree
{
    public abstract class UnityBehaviourCondition : MonoBehaviour, IBehaviourCondition
    {
        public abstract bool IsTrue();
    }
}