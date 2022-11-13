using UnityEngine;

namespace Elementary
{
    [CreateAssetMenu(
        fileName = "Scriptable Bool",
        menuName = "Elementary/New Scriptable Bool"
    )]
    public sealed class ScriptableBool : ScriptableObject
    {
        [SerializeField]
        public bool value;
    }
}