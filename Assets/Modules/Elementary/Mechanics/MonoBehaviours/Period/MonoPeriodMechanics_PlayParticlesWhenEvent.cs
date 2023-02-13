using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Mechanics/Period Mechanics «Play Particles When Event»")]
    public sealed class MonoPeriodMechanics_PlayParticlesWhenEvent : MonoPeriodMechanics
    {
        [SerializeField]
        private ParticleSystem vfx;

        protected override void OnPeriodEvent()
        {
            this.vfx.Play(withChildren: true);            
        }
    }
}