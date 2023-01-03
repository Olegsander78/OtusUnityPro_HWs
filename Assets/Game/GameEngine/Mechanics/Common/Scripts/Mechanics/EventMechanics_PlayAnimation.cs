using UnityEngine;

public sealed class EventMechanics_PlayAnimation : EventMechanics
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