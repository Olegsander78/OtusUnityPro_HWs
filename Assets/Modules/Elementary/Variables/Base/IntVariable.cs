using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class IntVariable : IVariable<int>
    {
        public event Action<int> OnValueChanged;

        public int Value
        {
            get { return this.value; }
            set { this.SetValue(value); }
        }

        private readonly List<IAction<int>> listeners = new();

        [OnValueChanged("SetValue")]
        [SerializeField]
        private int value;

        public void AddListener(IAction<int> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<int> listener)
        {
            this.listeners.Remove(listener);
        }

        private void SetValue(int value)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do(value);
            }

            this.value = value;
            this.OnValueChanged?.Invoke(value);
        }
    }
}