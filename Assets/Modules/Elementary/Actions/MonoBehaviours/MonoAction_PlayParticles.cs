using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Actions/Action «Particles -> Play»")]
    public sealed class MonoAction_PlayParticles : MonoAction
    {
        [Space, SerializeField]
        public ParticleSystem[] particleSystems;

        [Button, GUIColor(0, 1, 0)]
        public override void Do()
        {
            for (int i = 0, count = this.particleSystems.Length; i < count; i++)
            {
                var particleSystem = this.particleSystems[i];
                particleSystem.Play(withChildren: true);
            }
        }
    }
}