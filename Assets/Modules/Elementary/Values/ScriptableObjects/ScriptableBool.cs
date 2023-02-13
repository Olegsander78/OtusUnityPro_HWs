using UnityEngine;

namespace Elementary
{
    [CreateAssetMenu(
        fileName = "Scriptable Bool",
        menuName = "Elementary/Values/New Scriptable Bool"
    )]
    public sealed class ScriptableBool : ScriptableObject, IValue<bool>
    {
        public bool Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private bool value;
    }
}