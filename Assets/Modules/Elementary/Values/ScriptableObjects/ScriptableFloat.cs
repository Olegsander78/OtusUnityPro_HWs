using UnityEngine;

namespace Elementary
{
    [CreateAssetMenu(
        fileName = "Scriptable Float",
        menuName = "Elementary/Values/New Scriptable Float"
    )]
    public sealed class ScriptableFloat : ScriptableObject, IValue<float>
    {
        public float Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private float value;
    }
}