using UnityEngine;

namespace Elementary
{
    public abstract class ConditionBehaviour : MonoBehaviour, ICondition
    {
        public abstract bool IsTrue();
    }

    public abstract class ConditionBehaviour<T> : MonoBehaviour, ICondition<T>
    {
        public abstract bool IsTrue(T value);
    }
}