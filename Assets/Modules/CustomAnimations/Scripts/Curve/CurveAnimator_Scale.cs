using System;
using UnityEngine;

namespace CustomAnimations
{
    [AddComponentMenu("CustomAnimations/Curve Animator «Scale»")]
    public sealed class CurveAnimator_Scale : CurveAnimator<Vector3>
    {
        [Space]
        [Header("Scale")]
        [SerializeField]
        private Transform root;

        private Vector3 defaultScale;

        protected override Vector3 DefaultValue
        {
            get { return this.defaultScale; }
        }

        protected override Func<float, Vector3> SumFunction
        {
            get { return this.sumFunction; }
        }

        protected override Func<float, Vector3> MultiplyFunction
        {
            get { return this.multiplyFunction; }
        }

        private Func<float, Vector3> sumFunction;

        private Func<float, Vector3> multiplyFunction;

        protected override void SetValue(Vector3 result)
        {
            this.root.localScale = result;
        }

        #region Lifecycle

        private void Awake()
        {
            this.defaultScale = this.root.localScale;
            this.sumFunction = term => this.defaultScale + new Vector3(term, term, term);
            this.multiplyFunction = multiplier => this.defaultScale * (1.0f + multiplier); 
        }

        #endregion
    }
}