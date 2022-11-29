using System;
using System.Collections;
using UnityEngine;

namespace CustomAnimations
{
    public abstract class CurveAnimator<T> : MonoBehaviour
    {
        protected abstract T DefaultValue { get; }
        
        [SerializeField]
        private CurveAnimation config;
        
        private Coroutine animationCoroutine;

        protected abstract Func<float, T> SumFunction { get; }

        protected abstract Func<float, T> MultiplyFunction { get; }

        public void SetAnimation(CurveAnimation animation)
        {
            this.config = animation;
        }

        public void Play()
        {
            this.ResetState();
            this.animationCoroutine = this.StartCoroutine(this.AnimateRoutine());
        }

        public void Stop()
        {
            if (this.animationCoroutine != null)
            {
                this.StopCoroutine(this.animationCoroutine);
                this.animationCoroutine = null;
            }
        }

        public void ResetState()
        {
            this.Stop();
            this.SetValue(this.DefaultValue);
        }

        protected abstract void SetValue(T result);

        private IEnumerator AnimateRoutine()
        {
            Func<float, T> function;
            if (this.config.functionType == CurveFunctionType.SUM)
            {
                function = this.SumFunction;
            }
            else
            {
                function = this.MultiplyFunction;
            }

            const float end = 1.0f;
            var progress = 0f;
            var dProgress = Time.deltaTime / this.config.duration;
            var curve = this.config.curve;

            while (progress < end)
            {
                progress = Mathf.Min(progress + dProgress, end);
                var curveValue = curve.Evaluate(progress);
                var value = function.Invoke(curveValue);
                this.SetValue(value);
                yield return null;
            }
        }
    }
}