using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using Elementary;
using UnityEngine;


public sealed class AnimationState_ChangeState : State
{
    [SerializeField]
    private AnimationSystem system;

    [SerializeField]
    private IntAdapter enterId;

    [Space]
    [SerializeField]
    private bool hasExitAnimation;

    [ShowIf("hasExitAnimation")]
    [OptionalField]
    [SerializeField]
    private IntAdapter exitId;

    public override void Enter()
    {
        this.system.ChangeState(this.enterId.Value);
    }

    public override void Exit()
    {
        if (this.hasExitAnimation)
        {
            this.system.ChangeState(this.exitId.Value);
        }
    }
}