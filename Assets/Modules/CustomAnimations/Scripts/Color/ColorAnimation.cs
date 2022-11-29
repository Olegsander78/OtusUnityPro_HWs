using UnityEngine;

namespace CustomAnimations
{
    [CreateAssetMenu(
        fileName = "Color Animation",
        menuName = "CustomAnimations/New Color Animation"
    )]
    public sealed class ColorAnimation : ScriptableObject
    {
        [SerializeField]
        public Gradient colorGradient;

        [SerializeField]
        public float duration = 0.2f;
    }
}