using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Actions/Action «Bool Variable -> False»")]
    public sealed class MonoAction_BoolVariable_False : MonoAction
    {
        [SerializeField]
        public MonoBoolVariable toggle;

        [Button, GUIColor(0, 1, 0)]
        public override void Do()
        {
            this.toggle.SetFalse();
        }
    }
}