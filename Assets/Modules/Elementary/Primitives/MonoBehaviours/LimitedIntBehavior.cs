using System;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Limited Int")]
    public sealed class LimitedIntBehavior : MonoBehaviour
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
        private LimitedInt source;
    }
}