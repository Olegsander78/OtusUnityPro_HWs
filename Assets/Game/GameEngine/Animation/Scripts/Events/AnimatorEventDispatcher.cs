using System;
using UnityEngine;
using Object = UnityEngine.Object;


[RequireComponent(typeof(Animator))]
public sealed class AnimatorEventDispatcher : MonoBehaviour
{
    public event StateDelegate OnStateEntered;

    public event StateDelegate OnStateExited;

    public event Action<AnimationClip> OnAnimationStarted;

    public event Action<AnimationClip> OnAnimationEnded;

    public event Action OnEventReceived;

    public event Action<bool> OnBoolReceived;

    public event Action<int> OnIntReceived;

    public event Action<float> OnFloatReceived;

    public event Action<string> OnStringReceived;

    public event Action<Object> OnObjectReceived;

    private Animator animator;

    public void OnEnterState(AnimatorStateInfo state, int stateId, int layerIndex)
    {
        this.OnStateEntered?.Invoke(state, stateId, layerIndex);
    }

    public void OnExitState(AnimatorStateInfo state, int stateId, int layerIndex)
    {
        this.OnStateExited?.Invoke(state, stateId, layerIndex);
    }

    public void ReceiveStartAnimation(AnimationClip clip)
    {
        this.OnAnimationStarted?.Invoke(clip);
    }

    public void ReceiveEndAnimation(AnimationClip clip)
    {
        this.OnAnimationEnded?.Invoke(clip);
    }

    public void ReceiveString(string message)
    {
        this.OnStringReceived?.Invoke(message);
    }

    public void ReceiveBool(bool message)
    {
        this.OnBoolReceived?.Invoke(message);
    }

    public void ReceiveInt(int message)
    {
        this.OnIntReceived?.Invoke(message);
    }

    public void ReceiveFloat(float message)
    {
        this.OnFloatReceived?.Invoke(message);
    }

    public void ReceiveReference(Object obj)
    {
        this.OnObjectReceived?.Invoke(obj);
    }

    public void ReceiveEvent()
    {
        this.OnEventReceived?.Invoke();
    }
}