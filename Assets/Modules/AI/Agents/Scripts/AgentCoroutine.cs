using System.Collections;
using UnityEngine;

namespace AI.Agents
{
    public abstract class AgentCoroutine : Agent
    {
        private readonly MonoBehaviour coroutineDispatcher;

        private readonly YieldInstruction framePeriod;

        private Coroutine coroutine;

        public AgentCoroutine(MonoBehaviour coroutineDispatcher, YieldInstruction framePeriod = null)
        {
            this.coroutineDispatcher = coroutineDispatcher;
            this.framePeriod = framePeriod;
        }

        protected override void OnStart()
        {
            this.coroutine = this.coroutineDispatcher.StartCoroutine(this.LoopCoroutine());
        }

        protected override void OnStop()
        {
            if (this.coroutine != null)
            {
                this.coroutineDispatcher.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }

        private IEnumerator LoopCoroutine()
        {
            while (true)
            {
                yield return this.framePeriod;
                this.Update();
            }
        }

        protected abstract void Update();
    }
}