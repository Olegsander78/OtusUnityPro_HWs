using System.Collections;
using UnityEngine;

namespace AI.BTree
{
    public abstract class BehaviourNode_Coroutine : BehaviourNode
    {
        public MonoBehaviour coroutineDispatcher;

        private Coroutine coroutine;

        protected BehaviourNode_Coroutine()
        {
        }

        protected BehaviourNode_Coroutine(MonoBehaviour coroutineDispatcher)
        {
            this.coroutineDispatcher = coroutineDispatcher;
        }

        protected sealed override void Run()
        {
            this.coroutine = this.coroutineDispatcher.StartCoroutine(this.RunRoutine());
        }

        protected abstract IEnumerator RunRoutine();

        protected override void OnEnd()
        {
            if (this.coroutine != null)
            {
                this.coroutineDispatcher.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }
    }
}