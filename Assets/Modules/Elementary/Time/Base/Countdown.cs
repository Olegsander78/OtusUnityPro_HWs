using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    public sealed class Countdown
    {
        public event Action OnStarted;

        public event Action OnTimeChanged;

        public event Action OnStopped;

        public event Action OnEnded;

        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-9)]
        [ProgressBar(0, 1)]
        public float Progress
        {
            get { return 1 - this.remainingTime / this.duration; }
            set { this.SetProgress(value); }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-7)]
        public float RemainingTime
        {
            get { return this.remainingTime; }
            set { this.remainingTime = Mathf.Clamp(value, 0, this.duration); }
        }

        private float duration;

        private float remainingTime;

        private readonly MonoBehaviour coroutineDispatcher;

        private Coroutine timerCoroutine;

        public Countdown(MonoBehaviour coroutineDispatcher, float duration)
        {
            this.coroutineDispatcher = coroutineDispatcher;
            this.duration = duration;
            this.remainingTime = duration;
        }

        public void Play()
        {
            if (this.IsPlaying)
            {
                return;
            }

            this.IsPlaying = true;
            this.OnStarted?.Invoke();
            this.timerCoroutine = this.coroutineDispatcher.StartCoroutine(this.TimerRoutine());
        }

        public void Stop()
        {
            if (this.timerCoroutine != null)
            {
                this.coroutineDispatcher.StopCoroutine(this.timerCoroutine);
                this.timerCoroutine = null;
            }

            if (this.IsPlaying)
            {
                this.IsPlaying = false;
                this.OnStopped?.Invoke();
            }
        }

        public void Reset()
        {
            this.remainingTime = this.duration;
            this.OnReset?.Invoke();
        }

        private IEnumerator TimerRoutine()
        {
            while (this.remainingTime > 0)
            {
                yield return null;
                this.remainingTime -= Time.deltaTime;
                this.OnTimeChanged?.Invoke();
            }

            this.IsPlaying = false;
            this.OnEnded?.Invoke();
        }

        private void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this.remainingTime = this.duration * (1 - progress);
            this.OnTimeChanged?.Invoke();
        }
    }
}