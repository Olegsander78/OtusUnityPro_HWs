using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Mechanics/Event Mechanics «Enable Bool Variable»")]
    public sealed class MonoEventMechanics_EnableBoolVariable : MonoEventMechanics
    {
        [SerializeField]
        private bool setEnable = true;
    
        [SerializeField]
        private MonoBoolVariable toggle;
        
        protected override void OnEvent()
        {
            this.toggle.SetValue(this.setEnable);
        }
    }
}