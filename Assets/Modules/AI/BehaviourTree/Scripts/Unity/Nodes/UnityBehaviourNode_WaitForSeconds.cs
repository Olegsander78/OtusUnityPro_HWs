using System.Collections;
using Elementary;
using UnityEngine;

namespace AI.BTree
{
    [AddComponentMenu(Extensions.MENU_PATH + "Node «Wait For Seconds»")]
    public sealed class UnityBehaviourNode_WaitForSeconds : UnityBehaviourNode_Coroutine
    {
        [SerializeField]
        private FloatAdapter waitSeconds;

        protected override IEnumerator RunRoutine()
        {
            yield return new WaitForSeconds(this.waitSeconds.Value);
            this.Return(true);
        }
    }
}