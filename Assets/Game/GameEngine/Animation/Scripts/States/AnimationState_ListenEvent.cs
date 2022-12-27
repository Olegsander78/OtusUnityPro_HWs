using System.Collections.Generic;
using Elementary;
using UnityEngine;
using UnityEngine.Events;


public sealed class AnimationState_ListenEvent : State
{
    [Space]
    [SerializeField]
    private AnimationSystem animationSystem;

    [SerializeField]
    private UnityEvent unityEvent;

    [Space]
    [SerializeField]
    private List<string> animationEvents = new()
    {
        "harvest"
    };

    public override void Enter()
    {
        this.animationSystem.OnStringReceived += this.OnAnimationEvent;
    }

    public override void Exit()
    {
        this.animationSystem.OnStringReceived -= this.OnAnimationEvent;
    }

    private void OnAnimationEvent(string message)
    {
        if (this.animationEvents.Contains(message))
        {            
            this.unityEvent.Invoke();
        }
        else
            Debug.Log($"{message} was not contained!");
    }
}