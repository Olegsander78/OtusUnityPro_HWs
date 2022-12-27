using Elementary;
using UnityEngine;


public sealed class AnimationState : State
{
    [SerializeField]
    private Animator animator;

    private static readonly int State = Animator.StringToHash("State");

    [SerializeField]
    private IntAdapter stateId;

    public override void Enter()
    {
        this.animator.SetInteger(State, this.stateId.Value);
    }
}