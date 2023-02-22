using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI.GOAP.Unity
{
    public abstract class CoroutineStateController : MonoStateController
    {
        [Header("Observe Delay")]
        [SerializeField]
        private float minRandomDelay;

        [SerializeField]
        private float maxRandomDelay;

        private Coroutine coroutine;

        protected void Activate()
        {
            if (this.coroutine == null)
            {
                this.UpdateState();
                this.coroutine = this.StartCoroutine(this.ObserveRoutine());
            }
        }

        protected void Deactivate()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }

        private void UpdateState()
        {
            var value = this.EvaluateValue();
            this.worldState.ChangeParameter(this.stateName, value);
        }

        private IEnumerator ObserveRoutine()
        {
            while (true)
            {
                var delay = Random.Range(this.maxRandomDelay, this.minRandomDelay);
                yield return new WaitForSeconds(delay);
                this.UpdateState();   
            }
        }

        protected abstract bool EvaluateValue();
    }
}