using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Actions/Action «Animator -> Play»")]
    public sealed class MonoAction_Animator_Play : MonoAction
    {
        [Space, SerializeField]
        public Animator animator;

        [Space, SerializeField]
        public string animationName;

        [SerializeField]
        public int layerIndex = -1;

        [SerializeField]
        public float normalizedTime = 0;

        [Button, GUIColor(0, 1, 0)]
        public override void Do()
        {
            this.animator.Play(this.animationName, this.layerIndex, this.normalizedTime);
        }
    }
}