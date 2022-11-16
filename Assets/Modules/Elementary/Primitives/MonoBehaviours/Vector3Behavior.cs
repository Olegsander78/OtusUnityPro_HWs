using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Int")]
    public sealed class Vector3Behavior : MonoBehaviour
    {
        public event Action<Vector3> OnValueChanged;

        public Vector3 Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                this.OnValueChanged?.Invoke(value);
            }
        }

        [SerializeField]
        private Vector3 value;

        [Title("Methods")]
        [GUIColor(0, 1, 0)]
        [Button]
        public void Assign(Vector3 value)
        {
            this.value = value;
            this.OnValueChanged?.Invoke(value);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void Plus(Vector3 range)
        {
            var newValue = this.value + range;
            this.Assign(newValue);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void Minus(Vector3 range)
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


