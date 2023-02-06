
public sealed class RealtimeRepository : DataRepository<RealtimeData>
{
    protected override string Key => "UserSessionData";

    public bool LoadSession(out RealtimeData data)
    {
        return this.LoadData(out data);
    }

    public void SaveSession(RealtimeData data)
    {
        this.SaveData(data);
    }
}