using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [AddComponentMenu("Entities/Entity Group")]
    public sealed class UnityEntityGroup : UnityEntityBase
    {
        [SerializeField]
        private UnityEntity[] entities = new UnityEntity[0];

        public override T Get<T>()
        {
            if (base.TryGet(out T result))
            {
                return result;
            }
            
            for (int i = 0, count = this.entities.Length; i < count; i++)
            {
                var entity = this.entities[i];
                if (entity.TryGet(out T element))
                {
                    return element;
                }
            }

            throw new Exception($"Element of type {typeof(T).Name} is not found!");
        }

        public override T[] GetAll<T>()
        {
            var rootElements = base.GetAll<T>();

            var list = new List<T>();
            list.AddRange(rootElements);
            
            for (int i = 0, count = this.entities.Length; i < count; i++)
            {
                var entity = this.entities[i];
                if (entity.TryGet(out T element))
                {
                    list.Add(element);
                }
            }

            return list.ToArray();
        }
        
        public override bool TryGet<T>(out T element)
        {
            if (base.TryGet(out element))
            {
                return true;
            }
            
            for (int i = 0, count = this.entities.Length; i < count; i++)
            {
                var entity = this.entities[i];
                if (entity.TryGet(out element))
                {
                    return true;
                }
            }

            return false;
        }
    }
}