using Elementary;
using UnityEngine;


public sealed class AnimationState_ApplyRootMotion : State
{
    [SerializeField]
    private AnimationSystem system;

    public override void Enter()
    {
        this.system.ApplyRootMotion();
    }
}