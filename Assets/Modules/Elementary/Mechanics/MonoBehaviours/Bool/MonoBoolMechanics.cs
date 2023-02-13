using UnityEngine;

namespace Elementary
{
    public abstract class MonoBoolMechanics : MonoBehaviour
    {
        [SerializeField]
        private MonoBoolVariable toggle;

        protected virtual void Awake()
        {
            this.SetEnable(this.toggle.Value);
        }

        protected virtual void OnEnable()
        {
            this.toggle.OnValueChanged += this.SetEnable;
        }

        protected virtual void OnDisable()
        {
            this.toggle.OnValueChanged -= this.SetEnable;
        }

        protected abstract void SetEnable(bool isEnable);
        
#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (this.toggle != null)
            {
                this.toggle.OnValueChanged -= this.SetEnable;
                this.toggle.OnValueChanged += this.SetEnable;
                this.SetEnable(this.toggle.Value);
            }
        }
#endif
    }
}