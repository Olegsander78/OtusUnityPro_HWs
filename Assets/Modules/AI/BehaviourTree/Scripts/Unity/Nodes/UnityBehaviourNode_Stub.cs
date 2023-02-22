using UnityEngine;

namespace AI.BTree
{
    [AddComponentMenu(Extensions.MENU_PATH + "Node «Stub»")]
    public sealed class UnityBehaviourNode_Stub : UnityBehaviourNode
    {
        [SerializeField]
        private bool success = true;
        
        protected override void Run()
        {
            this.Return(this.success);
        }
    }
}