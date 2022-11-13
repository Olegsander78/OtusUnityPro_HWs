using UnityEngine;

namespace Elementary
{
    public abstract class ScriptableCondition : ScriptableObject, ICondition
    {
        public abstract bool IsTrue();
    }

    public abstract class ScriptableCondition<T> : ScriptableObject, ICondition<T>
    {
        public abstract bool IsTrue(T value);
    }
}