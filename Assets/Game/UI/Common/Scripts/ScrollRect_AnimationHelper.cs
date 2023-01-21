using UIFrames.Unity;
using UnityEngine;
using UnityEngine.UI;


[DisallowMultipleComponent]
public sealed class ScrollRect_AnimationHelper : MonoBehaviour
{
    [SerializeField]
    private ScrollRect rect;

    [SerializeField]
    private UnityFrameAnimator animator;

    private void OnEnable()
    {
        this.animator.OnShowStarted += this.OnAnimationStarted;
        this.animator.OnHideStarted += this.OnAnimationStarted;
        this.animator.OnShowFinished += this.OnAnimationFinished;
        this.animator.OnHideFinished += this.OnAnimationFinished;
    }

    private void OnDisable()
    {
        this.animator.OnShowStarted -= this.OnAnimationStarted;
        this.animator.OnHideStarted -= this.OnAnimationStarted;
        this.animator.OnShowFinished -= this.OnAnimationFinished;
        this.animator.OnHideFinished -= this.OnAnimationFinished;
    }

    private void OnAnimationStarted()
    {
        this.rect.enabled = false;
    }

    private void OnAnimationFinished()
    {
        this.rect.enabled = true;
    }

    private void Reset()
    {
        this.rect = this.GetComponent<ScrollRect>();
    }
}