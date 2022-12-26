using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    public sealed class Period
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

        private readonly MonoBehaviour coroutineDispatcher;

        private readonly YieldInstruction period;

        private Coroutine coroutine;

        public Period(MonoBehaviour coroutineDispatcher, float period)
        {
            this.coroutineDispatcher = coroutineDispatcher;
            this.period = new WaitForSeconds(period);
        }

        public void Play()
        {
            if (this.coroutine == null)
            {
                this.coroutine = this.coroutineDispatcher.StartCoroutine(this.PeriodRoutine());
                this.OnStarted?.Invoke();
            }
        }

        public void Stop()
        {
            if (this.coroutine != null)
            {
                this.coroutineDispatcher.StopCoroutine(this.coroutine);
                this.coroutine = null;
                this.OnStoped?.Invoke();
            }
        }

        private IEnumerator PeriodRoutine()
        {
            while (true)
            {
                yield return this.period;
                this.OnPeriodEvent?.Invoke();
            }
        }
    }
}