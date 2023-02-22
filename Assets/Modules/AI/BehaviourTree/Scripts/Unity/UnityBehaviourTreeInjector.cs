using UnityEngine;

namespace AI.BTree
{
    public sealed class UnityBehaviourTreeInjector : MonoBehaviour
    {
        [SerializeField]
        private bool injectOnAwake;

        [SerializeField]
        private UnityBehaviourTree behaviourTree;

        private void Awake()
        {
            if (this.injectOnAwake)
            {
                this.InjectBehaviourTree();
            }
        }

        public void InjectBehaviourTree()
        {
            var injects = this.GetComponentsInChildren<IBehaviourTreeInjective>();
            for (int i = 0, count = injects.Length; i < count; i++)
            {
                var injections = injects[i];
                injections.Tree = this.behaviourTree;
            }
        }
    }
}