using UnityEngine;

namespace Entities
{
    public abstract class UnityEntity : MonoBehaviour, IEntity
    {
        public abstract T Get<T>();
        
        public abstract T[] GetAll<T>();
        
        public abstract bool TryGet<T>(out T element);
        
        public abstract object[] GetAll();
        
        public abstract void Add(object element);
        
        public abstract void Remove(object element);
    }
}