using System;
using System.Collections;
using UnityEngine;

namespace AI.BTree
{
    [Serializable]
    public sealed class BehaviourNode_WaitForSeconds : BehaviourNode_Coroutine
    {
        [SerializeField]
        public float waitSeconds;

        public BehaviourNode_WaitForSeconds()
        {
        }

        public BehaviourNode_WaitForSeconds(MonoBehaviour coroutineDispatcher) : base(coroutineDispatcher)
        {
        }

        protected override IEnumerator RunRoutine()
        {
            yield return new WaitForSeconds(this.waitSeconds);
            this.Return(true);
        }
    }
}