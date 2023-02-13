using UnityEngine;

namespace Elementary
{
    [CreateAssetMenu(
        fileName = "Scriptable String",
        menuName = "Elementary/Values/New Scriptable String"
    )]
    public sealed class ScriptableString : ScriptableObject, IValue<string>
    {
        public string Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private string value;
    }
}