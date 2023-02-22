using System.Collections;
using UnityEngine;

namespace AI.BTree
{
    public abstract class UnityBehaviourNode_Coroutine : UnityBehaviourNode
    {
        private Coroutine coroutine;

        protected sealed override void Run()
        {
            this.coroutine = this.StartCoroutine(this.RunRoutine());
        }

        protected abstract IEnumerator RunRoutine();

        protected override void OnEnd()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }
    }
}