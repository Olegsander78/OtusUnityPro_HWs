using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Variables/Float Variable")]
    public sealed class MonoFloatVariable : MonoBehaviour, IVariable<float>
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

        public void SetValue(float value)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do(value);
            }

            this.value = value;
            this.OnValueChanged?.Invoke(value);
        }

        public void Plus(float range)
        {
            var newValue = this.value + range;
            this.SetValue(newValue);
        }

        public void Minus(float range)
        {
            var newValue = this.value - range;
            this.SetValue(newValue);
        }

        public void Multiply(float multiplier)
        {
            var newValue = this.value * multiplier;
            this.SetValue(newValue);
        }

        public void Divide(float divider)
        {
            var newValue = this.value / divider;
            this.SetValue(newValue);
        }

        public void AddListener(IAction<float> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<float> listener)
        {
            this.listeners.Remove(listener);
        }
    }
}