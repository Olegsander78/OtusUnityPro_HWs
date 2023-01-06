using System.Collections.Generic;
using UnityEngine;

namespace CustomParticles
{
    public abstract class ParticlePool<T> : MonoBehaviour where T : Object
    {
        [SerializeField]
        private Transform pool;
        
        [Space]
        [SerializeField]
        private T particlePrefab;

        [SerializeField]
        private int initialSize = 16;

        private List<T> particlePool;

        protected virtual void Awake()
        {
            this.particlePool = new List<T>(this.initialSize);
            for (var i = 0; i < this.initialSize; i++)
            {
                var particle = Instantiate(this.particlePrefab, this.pool);
                this.particlePool.Add(particle);
            }

            this.pool.gameObject.SetActive(false);
        }

        public virtual T Get(Transform parent)
        {
            var count = this.particlePool.Count;
            if (count <= 0)
            {
                return Instantiate(this.particlePrefab, parent);
            }

            var lastIndex = count - 1;
            var particle = this.particlePool[lastIndex];
            this.particlePool.RemoveAt(lastIndex);

            var transform = this.GetTransform(particle);
            transform.SetParent(parent);
            return particle;
        }

        public virtual void Release(T particle)
        {
            var transform = this.GetTransform(particle);
            transform.SetParent(this.pool);
            this.particlePool.Add(particle);
        }

        protected abstract Transform GetTransform(T particle);
    }
}