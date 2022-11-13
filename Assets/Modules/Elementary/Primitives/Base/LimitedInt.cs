using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elementary
{
    [Serializable]
    public sealed class LimitedInt
    {
        public event Action<int> OnValueChanged;

        public event Action<int> OnMaxValueChanged;

        public int Value
        {
            get { return this.currentValue; }
            set { this.SetValue(value); }
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

        [SerializeField]
        private int maxValue;

        [SerializeField]
        private int currentValue;

        [Title("Methods")]
        [GUIColor(0, 1, 0)]
        [Button]
        private void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, this.maxValue);
            this.currentValue = value;
            this.OnValueChanged?.Invoke(this.currentValue);
        }

        [Button]
        [GUIColor(0, 1, 0)]
        private void SetMaxValue(int value)
        {
            value = Math.Max(0, value);
            if (this.currentValue > value)
            {
                this.currentValue = value;
                this.OnValueChanged.Invoke(value);
            }
            
            this.maxValue = value;
            this.OnMaxValueChanged?.Invoke(value);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.maxValue = Math.Max(1, this.maxValue);
            this.currentValue = Mathf.Clamp(this.currentValue, 1, this.maxValue);
        }
#endif
    }
}