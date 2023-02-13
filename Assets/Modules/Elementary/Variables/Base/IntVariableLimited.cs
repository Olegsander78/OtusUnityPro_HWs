using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class IntVariableLimited : IVariableLimited<int>
    {
        public event Action<int> OnValueChanged;

        public event Action<int> OnMaxValueChanged;

        public int Value
        {
            get { return this.currentValue; }
            set { this.SetValue(Mathf.Clamp(value, 0, this.maxValue)); }
        }

        public int MaxValue
        {
            get { return this.maxValue; }
            set { this.SetMaxValue(value); }
        }

        public bool IsLimit
        {
            get { return this.currentValue >= this.maxValue; }
        }

        private readonly List<IAction<int>> listeners = new();

        private readonly List<IAction<int>> maxListeners = new();

        [OnValueChanged("SetValue")]
        [SerializeField]
        private int currentValue;

        [OnValueChanged("SetMaxValue")]
        [SerializeField]
        private int maxValue;

        public void AddListener(IAction<int> listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction<int> listener)
        {
            this.listeners.Remove(listener);
        }

        public void AddMaxListener(IAction<int> listener)
        {
            this.maxListeners.Add(listener);
        }

        public void RemoveMaxListener(IAction<int> listener)
        {
            this.maxListeners.Remove(listener);
        }

        private void SetValue(int value)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do(value);
            }
            
            this.currentValue = value;
            this.OnValueChanged?.Invoke(this.currentValue);
        }
        
        private void SetMaxValue(int value)
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