using UnityEngine;

namespace Entities
{
    [AddComponentMenu("Entities/Entity Proxy")]
    public sealed class UnityEntityProxy : UnityEntity
    {
        [SerializeField]
        private UnityEntity entity;
        
        public override T Get<T>()
        {
            return this.entity.Get<T>();
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