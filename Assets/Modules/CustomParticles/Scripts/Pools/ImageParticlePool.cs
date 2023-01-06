using UnityEngine;

namespace CustomParticles
{
    public sealed class ImageParticlePool : ParticlePool<ImageParticle>
    {
        protected override Transform GetTransform(ImageParticle particle)
        {
            return particle.transform;
        }
    }
}