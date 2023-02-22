using UnityEngine;

namespace AI.BTree
{
    [AddComponentMenu(Extensions.MENU_PATH + "Node «Do And Inspect»")]
    public sealed class UnityBehaviourNode_DoAndInspect : UnityBehaviourNode, IBehaviourCallback
    {
        [SerializeField]
        private UnityBehaviourNode actionNode;

        [SerializeField]
        private UnityBehaviourNode[] inspectorNodes;

        protected override void Run()
        {
            this.actionNode.Run(callback: this);
            
            for (int i = 0, count = this.inspectorNodes.Length; i < count; i++)
            {
                var inspector = this.inspectorNodes[i];
                inspector.Run(callback: this);
            }
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            if (ReferenceEquals(node, this.actionNode))
            {
                this.Return(success);
            }
            else //Any inspector node
            {
                this.Return(false);
            }
        }

        protected override void OnAbort()
        {
            if (this.actionNode.IsRunning)
            {
                this.actionNode.Abort();
            }
            
            for (int i = 0, count = this.inspectorNodes.Length; i < count; i++)
            {
                var inspector = this.inspectorNodes[i];
                if (inspector.IsRunning)
                {
                    inspector.Abort();
                }
            }
        }
    }
}