using UnityEngine;

namespace CustomAnimations
{
    [CreateAssetMenu(
        fileName = "Curve Animation",
        menuName = "CustomAnimations/New Curve Animation"
    )]
    public sealed class CurveAnimation : ScriptableObject
    {
        [SerializeField]
        public AnimationCurve curve;

        [SerializeField]
        public float duration;

        [SerializeField]
        public CurveFunctionType functionType;
    }
}