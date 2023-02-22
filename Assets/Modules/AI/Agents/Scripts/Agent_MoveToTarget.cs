using System;
using UnityEngine;

namespace AI.Agents
{
    public abstract class Agent_MoveToTarget<T> : AgentCoroutine
    {
        public event Action<bool> OnTargetReached;

        public bool IsTargetReached
        {
            get { return this.isTargetReached; }
        }

        private T target;

        private bool isTargetReached;

        public Agent_MoveToTarget(MonoBehaviour coroutineDispatcher, YieldInstruction framePeriod) :
            base(coroutineDispatcher, framePeriod)
        {
        }

        public void SetTarget(T target)
        {
            this.target = target;
        }

        protected override void OnStart()
        {
            base.OnStart();
            this.isTargetReached = false;
        }

        protected override void Update()
        {
            var isTargetReached = this.CheckTargetReached(this.target);

            if (isTargetReached && !this.isTargetReached)
            {
                this.isTargetReached = true;
                this.OnTargetReached?.Invoke(true);
            }
            else if (!isTargetReached && this.isTargetReached)
            {
                this.isTargetReached = false;
                this.OnTargetReached?.Invoke(false);
            }

            if (!isTargetReached)
            {
                this.MoveToTarget(this.target);
            }
        }

        protected abstract bool CheckTargetReached(T target);

        protected abstract void MoveToTarget(T target);
    }
}