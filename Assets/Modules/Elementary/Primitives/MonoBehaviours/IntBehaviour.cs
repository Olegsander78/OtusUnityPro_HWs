using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Int")]
    public sealed class IntBehaviour : MonoBehaviour
    {
        public event Action<int> OnValueChanged;

        public int Value
        {
            get { return this.value; }
        }
        
        [SerializeField]
        private int value;

        [Title("Methods")]
        [GUIColor(0, 1, 0)]
        [Button]
        public void Assign(int value)
        {
            this.value = value;
            this.OnValueChanged?.Invoke(value);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void Plus(int range)
        {
            var newValue = this.value + range;
            this.Assign(newValue);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void Minus(int range)
        {
            var newValue = this.value - range;
            this.Assign(newValue);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void Multiply(int multiplier)
        {
            var newValue = this.value * multiplier;
            this.Assign(newValue);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void Divide(int divider)
        {
            var newValue = this.value / divider;
            this.Assign(newValue);
        }
    }
}