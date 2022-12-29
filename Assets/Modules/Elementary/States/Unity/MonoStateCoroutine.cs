using System.Collections;
using UnityEngine;

namespace Elementary
{
    public abstract class MonoStateCoroutine : MonoState
    {
        private Coroutine coroutine;
        
        public override void Enter()
        {
            if (this.coroutine == null)
            {
                this.coroutine = this.StartCoroutine(this.Do());
            }
        }

        public override void Exit()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }

        protected abstract IEnumerator Do();
    }
}