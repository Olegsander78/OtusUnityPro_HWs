namespace Entities
{
    public interface IEntity
    {
        T Get<T>();

        T[] GetAll<T>();

        bool TryGet<T>(out T element);

        object[] GetAll();
        
        void Add(object element);

        void Remove(object element);
    }
}