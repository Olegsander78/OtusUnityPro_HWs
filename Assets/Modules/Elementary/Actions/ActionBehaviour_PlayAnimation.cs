using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elementary
{
    [AddComponentMenu("Elementary/Action «Play Animation»")]
    public sealed class ActionBehaviour_PlayAnimation : ActionBehaviour 
    {
        [Space]
        [SerializeField]
        private Animator animator;

        [Space]
        [SerializeField]
        private string animationName;

        [SerializeField]
        private int layerIndex = -1;

        [SerializeField]
        private float normalizedTime = 0;

        [GUIColor(0, 1, 0)]
        [Button]
        public override void Do()
        {
            this.animator.Play(this.animationName, this.layerIndex, this.normalizedTime);
        }
    }
}