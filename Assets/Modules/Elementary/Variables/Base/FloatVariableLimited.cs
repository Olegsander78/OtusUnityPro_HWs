using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class FloatVariableLimited : IVariableLimited<float>
    {
        public event Action<float> OnValueChanged;

        public event Action<float> OnMaxValueChanged;

        public float Value
        {
            get { return this.currentValue; }
            set { this.SetValue(Mathf.Clamp(value, 0, this.maxValue)); }
        }

        public float MaxValue
        {
            get { return this.maxValue; }
            set { this.SetMaxValue(value); }
        }

        public bool IsLimit
        {
            get { return this.currentValue >= this.maxValue; }
        }

        private readonly List<IAction<float>> listeners = new();

        private readonly List<IAction<float>> maxListeners = new();

        [OnValueChanged("SetValue")]
        [SerializeField]
        private float currentValue;

        [OnValueChanged("SetMaxValue")]
        [SerializeField]
        private float maxValue;

        public void AddListener(IAction<float> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<float> listener)
        {
            this.listeners.Remove(listener);
        }

        public void AddMaxListener(IAction<float> listener)
        {
            this.maxListeners.Add(listener);
        }

        public void RemoveMaxListener(IAction<float> listener)
        {
            this.maxListeners.Remove(listener);
        }

        private void SetValue(float value)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do(value);
            }
            
            this.currentValue = value;
            this.OnValueChanged?.Invoke(this.currentValue);
        }
        
        private void SetMaxValue(float value)
        {
            value = Math.Max(1, value);
            if (this.currentValue > value)
            {
                this.SetValue(value);
            }

            for (int i = 0, count = this.maxListeners.Count; i < count; i++)
            {
                var listener = this.maxListeners[i];
                listener.Do(value);
            }
            
            this.maxValue = value;
            this.OnMaxValueChanged?.Invoke(value);
        }
    }
}