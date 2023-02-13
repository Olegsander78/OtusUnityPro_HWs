using System;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Variables/Int Variable «Limited»")]
    public sealed class MonoIntVariableLimited : MonoBehaviour, IVariableLimited<int>
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
        private IntVariableLimited source = new();

        public void AddListener(IAction<int> listener)
        {
            this.source.AddListener(listener);
        }

        public void RemoveListener(IAction<int> listener)
        {
            this.source.RemoveListener(listener);
        }


        public void AddMaxListener(IAction<int> listener)
        {
            this.source.AddMaxListener(listener);
        }

        public void RemoveMaxListener(IAction<int> listener)
        {
            this.source.RemoveMaxListener(listener);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.source.MaxValue = Math.Max(1, this.source.MaxValue);
            this.source.Value = Mathf.Clamp(this.source.Value, 0, this.source.MaxValue);
        }
#endif
    }
}