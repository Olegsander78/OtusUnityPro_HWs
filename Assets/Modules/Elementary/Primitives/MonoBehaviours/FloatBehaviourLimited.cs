using System;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Float «Limited»")]
    public sealed class FloatBehaviourLimited : MonoBehaviour
    {
        public event Action<float> OnValueChanged
        {
            add { this.source.OnValueChanged += value; }
            remove { this.source.OnValueChanged -= value; }
        }

        public event Action<float> OnMaxValueChanged
        {
            add { this.source.OnMaxValueChanged += value; }
            remove { this.source.OnMaxValueChanged -= value; }
        }

        public float Value
        {
            get { return this.source.Value; }
            set { this.source.Value = value; }
        }

        public float MaxValue
        {
            get { return this.source.MaxValue; }
            set { this.source.MaxValue = value; }
        }

        public bool IsLimit
        {
            get { return this.source.IsLimit; }
        }

        [SerializeField]
        private FloatLimited source = new();
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            this.source.MaxValue = Mathf.Max(1, this.source.MaxValue);
            this.source.Value = Mathf.Clamp(this.source.Value, 0, this.source.MaxValue);
        }
#endif
    }
}