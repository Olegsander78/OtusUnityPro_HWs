using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CustomAnimations
{
    [AddComponentMenu("CustomAnimations/Color Animator")]
    public sealed class ColorAnimator : MonoBehaviour
    {
        [SerializeField]
        private Graphic[] graphics;

        [SerializeField]
        private Color defaultColor = Color.white;

        [SerializeField]
        private ColorAnimation config;

        private Coroutine animationCoroutine;

        public void SetAnimation(ColorAnimation animation)
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
            this.SetColor(this.defaultColor);
        }

        private IEnumerator AnimateRoutine()
        {
            const float end = 1.0f;
            var progress = 0f;
            var dProgress = Time.deltaTime / this.config.duration;
            var gradient = this.config.colorGradient;

            while (progress < end)
            {
                progress = Mathf.Min(progress + dProgress, end);
                var color = gradient.Evaluate(progress);
                this.SetColor(color);
                yield return null;
            }
        }
        
        private void SetColor(Color color)
        {
            foreach (var graphic in this.graphics)
            {
                graphic.color = color;
            }
        }
    }
}