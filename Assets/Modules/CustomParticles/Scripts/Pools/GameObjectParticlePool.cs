using UnityEngine;

namespace CustomParticles
{
    public sealed class GameObjectParticlePool : ParticlePool<GameObject>
    {
        protected override Transform GetTransform(GameObject particle)
        {
            return particle.transform;
        }
    }
}