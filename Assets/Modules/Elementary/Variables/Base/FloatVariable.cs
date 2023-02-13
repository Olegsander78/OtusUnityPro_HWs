using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class FloatVariable : IVariable<float>
    {
        public event Action<float> OnValueChanged;

        public float Value
        {
            get { return this.value; }
            set { this.SetValue(value); }
        }

        private readonly List<IAction<float>> listeners = new();

        [OnValueChanged("SetValue")]
        [SerializeField]
        private float value;

        public void AddListener(IAction<float> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<float> listener)
        {
            this.listeners.Remove(listener);
        }

        private void SetValue(float value)
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