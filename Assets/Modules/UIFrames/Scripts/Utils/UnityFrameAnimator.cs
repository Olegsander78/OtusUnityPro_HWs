using System;
using UnityEngine;
using UnityEngine.Events;

namespace UIFrames.Unity
{
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    [AddComponentMenu("UIFrames/Frame Animator")]
    public sealed class UnityFrameAnimator : MonoBehaviour
    {
        public event Action OnShowStarted;

        public event Action OnShowFinished;

        public event Action OnHideStarted;

        public event Action OnHideFinished;

        private const string SHOW_ANIMATION_NAME = "Show";

        private const string HIDE_ANIMATION_NAME = "Hide";

        [SerializeField]
        private Animator animator;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onShown;

        [SerializeField]
        private UnityEvent onHidden;

        #region Events

        public void Show()
        {
            this.OnShowStarted?.Invoke();
            this.animator.Play(SHOW_ANIMATION_NAME, -1, 0);
        }

        public void Hide()
        {
            this.OnHideStarted?.Invoke();
            this.animator.Play(HIDE_ANIMATION_NAME, -1, 0);
        }

        private void OnShown()
        {
            this.onShown?.Invoke();
            this.OnShowFinished?.Invoke();
        }

        private void OnHidden()
        {
            this.onHidden?.Invoke();
            this.OnHideFinished?.Invoke();
        }

        #endregion

#if UNITY_EDITOR
        private void Reset()
        {
            this.animator = this.GetComponent<Animator>();
        }
#endif
    }
}