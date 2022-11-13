using UnityEngine;

namespace Elementary
{
    [CreateAssetMenu(
        fileName = "Scriptable String",
        menuName = "Elementary/New Scriptable String"
    )]
    public sealed class ScriptableString : ScriptableObject
    {
        [SerializeField]
        public string value;
    }
}