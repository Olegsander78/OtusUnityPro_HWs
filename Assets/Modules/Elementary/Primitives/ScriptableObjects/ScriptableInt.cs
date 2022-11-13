using UnityEngine;

namespace Elementary
{
    [CreateAssetMenu(
        fileName = "Scriptable Int",
        menuName = "Elementary/New Scriptable Int"
    )]
    public sealed class ScriptableInt : ScriptableObject
    {
        [SerializeField]
        public int value;
    }
}