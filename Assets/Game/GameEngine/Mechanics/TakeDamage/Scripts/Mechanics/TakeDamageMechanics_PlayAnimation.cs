using UnityEngine;


public sealed class TakeDamageMechanics_PlayAnimation : TakeDamageMechanics
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string animationName = "GetHit";

    protected override void OnDamageTaken(TakeDamageEvent damageEvent)
    {
        this.animator.Play(this.animationName, -1, 0);
    }
}