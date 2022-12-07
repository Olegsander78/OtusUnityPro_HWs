
public abstract class DataRepository<T>
{
    protected abstract string Key { get; }

    protected bool LoadData(out T data)
    {
        if (Database.KeyExists(this.Key))
        {
            data = Database.Load<T>(this.Key);
            return true;
        }

        data = default;
        return false;
    }

    protected virtual void SaveData(T data)
    {
        Database.Save<T>(this.Key, data);
    }
}