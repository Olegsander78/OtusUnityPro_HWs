using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Variables/Bool Variable")]
    public sealed class MonoBoolVariable : MonoBehaviour, IVariable<bool>
    {
        public event Action<bool> OnValueChanged;

        public bool Value
        {
            get { return this.value; }
            set { this.SetValue(value); }
        }

        private readonly List<IAction<bool>> listeners = new();

        [OnValueChanged("SetValue")]
        [SerializeField]
        private bool value;

        public void SetValue(bool value)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do(value);
            }

            this.value = value;
            this.OnValueChanged?.Invoke(value);
        }

        public void SetTrue()
        {
            this.SetValue(true);
        }

        public void SetFalse()
        {
            this.SetValue(false);
        }

        public void AddListener(IAction<bool> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<bool> listener)
        {
            this.listeners.Remove(listener);
        }
    }
}