using UnityEngine;

namespace CustomParticles
{
    public sealed class SpriteRendererParticlePool : ParticlePool<SpriteRendererParticle>
    {
        protected override Transform GetTransform(SpriteRendererParticle particle)
        {
            return particle.transform;
        }
    }
}