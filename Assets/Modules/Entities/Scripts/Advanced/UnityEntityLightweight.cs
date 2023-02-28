using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [AddComponentMenu("Entities/Lightweight Entity")]
    public class UnityEntityLightweight : UnityEntity, ISerializationCallbackReceiver
    {
        [SerializeReference]
        private List<object> elements = new();
        
        private Entity entity;

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
        
        public virtual void OnAfterDeserialize()
        {
            this.entity = new Entity(this.elements);
        }

        public virtual void OnBeforeSerialize()
        {
        }
    }
}