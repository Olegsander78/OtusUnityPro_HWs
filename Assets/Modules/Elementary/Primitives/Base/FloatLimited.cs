using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class FloatLimited
    {
        public event Action<float> OnValueChanged;

        public event Action<float> OnMaxValueChanged;

        public float Value
        {
            get { return this.currentValue; }
            set { this.SetValue(value); }
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

        [SerializeField]
        private float maxValue;

        [SerializeField]
        private float currentValue;

        [Title("Methods")]
        [GUIColor(0, 1, 0)]
        [Button]
        private void SetValue(float value)
        {
            value = Mathf.Clamp(value, 0, this.maxValue);
            this.currentValue = value;
            this.OnValueChanged?.Invoke(this.currentValue);
        }

        [Button]
        [GUIColor(0, 1, 0)]
        private void SetMaxValue(float value)
        {
            value = Math.Max(0, value);
            if (this.currentValue > value)
            {
                this.currentValue = value;
                this.OnValueChanged?.Invoke(value);
            }
            
            this.maxValue = value;
            this.OnMaxValueChanged?.Invoke(value);
        }
    }
}