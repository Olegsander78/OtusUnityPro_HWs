using UnityEngine;


public sealed class DestroyMechanics_PlayAnimation : DestroyMechanics
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string animationName = "Destroy";

    protected override void OnDestroyEvent(DestroyEvent destroyEvent)
    {
        this.animator.Play(this.animationName, -1, 0);
    }
}