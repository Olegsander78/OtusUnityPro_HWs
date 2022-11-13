using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Period")]
    public sealed class PeriodBehaviour : MonoBehaviour
    {
        public event Action OnStarted;

        public event Action OnPeriodEvent;

        public event Action OnStoped;

        [PropertyOrder(-10)]
        [PropertySpace]
        [ReadOnly]
        [ShowInInspector]
        public bool IsActive
        {
            get { return this.coroutine != null; }
        }

        private Coroutine coroutine;

        [Tooltip("Period in seconds")]
        [SerializeField]
        private float period = 1.0f;

        public void Play()
        {
            if (this.coroutine == null)
            {
                this.coroutine = this.StartCoroutine(this.PeriodRoutine());
                this.OnStarted?.Invoke();
            }
        }

        public void Stop()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
                this.OnStoped?.Invoke();
            }
        }

        private IEnumerator PeriodRoutine()
        {
            var period = new WaitForSeconds(this.period);
            while (true)
            {
                yield return period;
                this.OnPeriodEvent?.Invoke();
            }
        }
    }
}