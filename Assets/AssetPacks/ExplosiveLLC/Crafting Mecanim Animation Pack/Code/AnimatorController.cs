using UnityEngine;

namespace CraftingAnims
{
    //Placeholder functions for Animation events and Root Motion.
	public class AnimatorController : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
		[HideInInspector] public CrafterController crafterController;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Hit()
        {
        }

        public void Shoot()
        {
        }

        public void FootR()
        {
        }

        public void FootL()
        {
        }

        public void Land()
        {
        }

        public void WeaponSwitch()
        {
        }

        void OnAnimatorMove()
        {
            if(animator)
            {
				if(crafterController.isLocked)
				{
					transform.parent.rotation = animator.rootRotation;
					transform.parent.position += animator.deltaPosition;
				}
			}
        }
    }
}