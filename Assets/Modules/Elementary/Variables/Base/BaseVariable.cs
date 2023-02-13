using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class BaseVariable<T> : IVariable<T>
    {
        public event Action<T> OnValueChanged;

        public T Value
        {
            get { return this.value; }
            set { this.SetValue(value); }
        }

        private readonly List<IAction<T>> listeners = new();

        [SerializeField]
        private T value;

        private void SetValue(T value)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do(value);
            }

            this.value = value;
            this.OnValueChanged?.Invoke(value);
        }

        public void AddListener(IAction<T> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<T> listener)
        {
            this.listeners.Remove(listener);
        }
    }
}