using UnityEngine;

namespace Elementary
{
    [CreateAssetMenu(
        fileName = "Scriptable Int",
        menuName = "Elementary/Values/New Scriptable Int"
    )]
    public sealed class ScriptableInt : ScriptableObject, IValue<int>
    {
        public int Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private int value;
    }
}