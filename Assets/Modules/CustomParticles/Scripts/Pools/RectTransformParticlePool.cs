using UnityEngine;

namespace CustomParticles
{
    public sealed class RectTransformParticlePool : ParticlePool<RectTransform>
    {
        protected override Transform GetTransform(RectTransform particle)
        {
            return particle;
        }
    }
}