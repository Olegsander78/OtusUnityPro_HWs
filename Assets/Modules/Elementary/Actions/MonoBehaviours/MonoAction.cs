using UnityEngine;

namespace Elementary
{
    public abstract class MonoAction : MonoBehaviour, IAction
    {
        public abstract void Do();
    }

    public abstract class MonoAction<T> : MonoBehaviour, IAction<T>
    {
        public abstract void Do(T args);
    }
}