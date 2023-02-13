using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Mechanics/Event Mechanics «Play Animation»")]
    public sealed class MonoEventMechanics_PlayAnimation : MonoEventMechanics
    {
        [SerializeField]
        private Animator animator;

        [Space]
        [SerializeField]
        private string animationName = "hit";

        protected override void OnEvent()
        {
            this.animator.Play(this.animationName, -1, 0);
        }
    }
}