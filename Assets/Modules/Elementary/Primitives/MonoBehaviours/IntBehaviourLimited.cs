using System;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Integer «Limited»")]
    public sealed class IntBehaviourLimited : MonoBehaviour
    {
        public event Action<int> OnValueChanged
        {
            add { this.source.OnValueChanged += value; }
            remove { this.source.OnValueChanged -= value; }
        }

        public event Action<int> OnMaxValueChanged
        {
            add { this.source.OnMaxValueChanged += value; }
            remove { this.source.OnMaxValueChanged -= value; }
        }

        public int Value
        {
            get { return this.source.Value; }
            set { this.source.Value = value; }
        }

        public int MaxValue
        {
            get { return this.source.MaxValue; }
            set { this.source.MaxValue = value; }
        }

        public bool IsLimit
        {
            get { return this.source.IsLimit; }
        }

        [SerializeField]
        private IntLimited source = new();
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            this.source.MaxValue = Math.Max(1, this.source.MaxValue);
            this.source.Value = Mathf.Clamp(this.source.Value, 0, this.source.MaxValue);
        }
#endif
    }
}