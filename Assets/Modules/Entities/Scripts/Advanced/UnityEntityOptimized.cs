using UnityEngine;

namespace Entities
{
    [AddComponentMenu("Entities/Optimized Entity")]
    public sealed class UnityEntityOptimized : UnityEntity
    {
        private readonly Entity entity = new();

        public override T Get<T>()
        {
            try
            {
                return this.entity.Get<T>();
            }
            catch (EntityException exception)
            {
                Debug.LogError(exception.Message, this);
                throw;
            }
        }

        public override T[] GetAll<T>()
        {
            return this.entity.GetAll<T>();
        }

        public override object[] GetAll()
        {
            return this.entity.GetAll();
        }

        public override void Add(object element)
        {
            this.entity.Add(element);
        }

        public override void Remove(object element)
        {
            this.entity.Remove(element);
        }

        public override bool TryGet<T>(out T element)
        {
            return this.entity.TryGet(out element);
        }
    }
}