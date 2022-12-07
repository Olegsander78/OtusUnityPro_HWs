
public abstract class DataArrayRepository<T>
{
    protected abstract string Key { get; }

    protected bool LoadData(out T[] dataArray)
    {
        if (Database.KeyExists(this.Key))
        {
            dataArray = Database.Load<T[]>(this.Key);
            return true;
        }

        dataArray = null;
        return false;
    }

    protected void SaveData(T[] data)
    {
        Database.Save(this.Key, data);
    }
}