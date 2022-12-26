using Elementary;
using UnityEngine;


public sealed class AnimationState_ResetRootMotion : State
{
    [SerializeField]
    private AnimationSystem system;

    [Space]
    [SerializeField]
    private bool resetPosition = true;

    [SerializeField]
    private bool resetRotation = true;

    public override void Enter()
    {
        this.system.ResetRootMotion(
            resetPosition: this.resetPosition,
            resetRotation: this.resetRotation
        );
    }
}