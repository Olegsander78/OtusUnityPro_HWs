using System;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Toggle")]
    public sealed class BoolBehaviour : MonoBehaviour
    {
        public event Action<bool> OnValueChanged;

        public bool Value
        {
            get { return this.value; }
        }
        
        [SerializeField]
        private bool value;

        public void Assign(bool value)
        {
            this.value = value;
            this.OnValueChanged?.Invoke(value);
        }

        public void AssignTrue()
        {
            this.Assign(true);
        }

        public void AssignFalse()
        {
            this.Assign(false);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.Assign(this.value);
        }
#endif
    }
}