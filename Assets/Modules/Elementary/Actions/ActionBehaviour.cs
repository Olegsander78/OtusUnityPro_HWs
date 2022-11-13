using UnityEngine;

namespace Elementary
{
    public abstract class ActionBehaviour : MonoBehaviour, IAction
    {
        public abstract void Do();
    }

    public abstract class ActionBehaviour<T> : MonoBehaviour, IAction<T>
    {
        public abstract void Do(T args);
    }
}