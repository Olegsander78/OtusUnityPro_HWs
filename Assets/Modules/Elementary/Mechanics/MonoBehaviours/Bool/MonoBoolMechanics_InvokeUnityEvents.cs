using UnityEngine;
using UnityEngine.Events;

namespace Elementary
{
    [AddComponentMenu("Elementary/Mechanics/Bool Mechanics «Invoke Unity Events»")]
    public sealed class MonoBoolMechanics_InvokeUnityEvents : MonoBoolMechanics
    {
        [Space]
        [SerializeField]
        private UnityEvent onEnable;

        [SerializeField]
        private UnityEvent onDisable;

        [SerializeField]
        private UnityEvent<bool> onEvent;

        protected override void SetEnable(bool isEnable)
        {
            if (isEnable)
            {
                this.onEnable?.Invoke();
            }
            else
            {
                this.onDisable?.Invoke();
            }
            
            this.onEvent.Invoke(isEnable);
        }
    }
}