using System.Collections;
using Asyncoroutine;
using UnityEngine;

namespace AI.BTree
{
    [AddComponentMenu(Extensions.MENU_PATH + "Node «Endless Loop»")]
    public sealed class UnityBehaviourNode_EndlessLoop : UnityBehaviourNode, IBehaviourCallback
    {
        [SerializeField]
        private UnityBehaviourNode child;

        protected override void Run()
        {
            this.child.Run(callback: this);
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            this.StartCoroutine(this.RunInNextFrame());
        }

        private IEnumerator RunInNextFrame()
        {
            yield return new WaitForNextFrame();
            this.child.Run(callback: this);
        }

        protected override void OnAbort()
        {
            this.StopAllCoroutines();
            if (this.child.IsRunning)
            {
                this.child.Abort();
            }
        }
    }
}