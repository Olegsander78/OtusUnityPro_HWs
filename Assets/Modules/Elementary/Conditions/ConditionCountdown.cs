using System.Collections;
using UnityEngine;

namespace Elementary
{
    public sealed class ConditionCountdown : ICondition
    {
        private readonly MonoBehaviour coroutineDispatcher;
        
        private float remainingSeconds;

        public ConditionCountdown(MonoBehaviour coroutineDispatcher, float seconds, bool startInstantly)
        {
            this.remainingSeconds = seconds;
            this.coroutineDispatcher = coroutineDispatcher;
            
            if (startInstantly)
            {
                this.StartCountdown();
            }
        }

        public bool IsTrue()
        {
            return this.remainingSeconds <= 0.0f;
        }

        public void StartCountdown()
        {
            this.coroutineDispatcher.StartCoroutine(this.CountdownRoutine());
        }

        private IEnumerator CountdownRoutine()
        {
            while (this.remainingSeconds > 0.0f)
            {
                this.remainingSeconds -= Time.deltaTime;
                yield return null;
            }
        }
    }
}