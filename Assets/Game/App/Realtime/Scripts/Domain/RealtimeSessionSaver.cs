using Services;


public sealed class RealtimeSessionSaver :
    IAppStartListener,
    IAppStopListener
{
    [ServiceInject]
    private RealtimeRepository repository;

    [ServiceInject]
    private RealtimeManager realtimeManager;

    void IAppStartListener.Start()
    {
        this.realtimeManager.OnPaused += this.SaveSession;
        this.realtimeManager.OnEnded += this.SaveSession;
    }

    void IAppStopListener.Stop()
    {
        this.realtimeManager.OnPaused -= this.SaveSession;
        this.realtimeManager.OnEnded -= this.SaveSession;
    }

    private void SaveSession()
    {
        var data = new RealtimeData
        {
            nowSeconds = this.realtimeManager.RealtimeSeconds
        };
        this.repository.SaveSession(data);
    }
}