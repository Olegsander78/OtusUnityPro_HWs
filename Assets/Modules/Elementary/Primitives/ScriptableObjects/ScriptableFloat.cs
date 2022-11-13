using UnityEngine;

namespace Elementary
{
    [CreateAssetMenu(
        fileName = "Scriptable Float",
        menuName = "Elementary/New Scriptable Float"
    )]
    public sealed class ScriptableFloat : ScriptableObject
    {
        [SerializeField]
        public float value;
    }
}