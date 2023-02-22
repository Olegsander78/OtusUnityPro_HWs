using System.Collections.Generic;
using UnityEngine;

namespace AI.BTree
{
    [AddComponentMenu(Extensions.MENU_PATH + "Parallel Node")]
    public class UnityBehaviourNode_Parallel : UnityBehaviourNode_ParallelBase
    {
        protected override UnityBehaviourNode[] Children
        {
            get { return this.children; }
        }

        private UnityBehaviourNode[] children;

        private void Awake()
        {
            var children = new List<UnityBehaviourNode>();
            foreach (Transform child in this.transform)
            {
                if (child.gameObject.activeSelf && child.TryGetComponent(out UnityBehaviourNode node))
                {
                    children.Add(node);
                }
            }

            this.children = children.ToArray();
        }
    }
}