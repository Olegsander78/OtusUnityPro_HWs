using UnityEngine;

namespace Elementary
{
    public abstract class MonoPeriodMechanics : MonoBehaviour
    {
        [SerializeField]
        private MonoPeriod behaviour;

        protected virtual void OnEnable()
        {
            this.behaviour.OnStarted += this.OnStarted;
            this.behaviour.OnPeriodEvent += this.OnPeriodEvent;
            this.behaviour.OnStoped += this.OnStopped;
        }
        
        protected virtual void OnDisable()
        {
            this.behaviour.OnStarted -= this.OnStarted;
            this.behaviour.OnPeriodEvent -= this.OnPeriodEvent;
            this.behaviour.OnStoped -= this.OnStopped;
        }

        protected virtual void OnStarted()
        {
        }

        protected virtual void OnStopped()
        {
        }

        protected virtual void OnPeriodEvent()
        {
        }
    }
}