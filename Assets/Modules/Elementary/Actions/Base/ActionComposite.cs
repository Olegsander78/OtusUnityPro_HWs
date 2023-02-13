using System;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class ActionComposite<T> : IAction<T>
    {
        [SerializeField]
        private IAction<T>[] actions;

        public ActionComposite()
        {
            this.actions = new IAction<T>[0];
        }

        public ActionComposite(IAction<T>[] actions)
        {
            this.actions = actions;
        }

        public void Do(T arg)
        {
            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                var action = this.actions[i];
                action.Do(arg);
            }
        }
    }
}