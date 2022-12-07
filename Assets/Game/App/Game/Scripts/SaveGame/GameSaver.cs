using Services;


public sealed class GameSaver :
    IGameStartListener,
    IGameStopListener
{
    private const float SAVE_PERIOD_IN_SECONDS = 30;

    private ApplicationManager applicationManager;

    private IGameSaveListener[] listeners;

    private float remainingSeconds;

    [Inject]
    public void Construct(ApplicationManager applicationManager, IGameSaveListener[] listeners)
    {
        this.applicationManager = applicationManager;
        this.listeners = listeners;
    }

    void IGameStartListener.OnStartGame(GameManager gameManager)
    {
        this.remainingSeconds = SAVE_PERIOD_IN_SECONDS;

        this.applicationManager.OnUpdate += this.OnApplicationUpdate;
        this.applicationManager.OnPaused += this.OnApplicationPaused;
        this.applicationManager.OnQuit += this.OnQuitApplication;
    }

    void IGameStopListener.OnStopGame(GameManager gameManager)
    {
        this.applicationManager.OnUpdate -= this.OnApplicationUpdate;
        this.applicationManager.OnPaused -= this.OnApplicationPaused;
        this.applicationManager.OnQuit -= this.OnQuitApplication;
    }

    private void OnApplicationUpdate(float deltaTime)
    {
        this.remainingSeconds -= deltaTime;
        if (this.remainingSeconds <= 0.0f)
        {
            this.NotifyAboutSave(GameSaveReason.TIMER);
        }
    }

    private void OnApplicationPaused()
    {
        this.NotifyAboutSave(GameSaveReason.PAUSE);
    }

    private void OnQuitApplication()
    {
        this.NotifyAboutSave(GameSaveReason.QUIT);
    }

    private void NotifyAboutSave(GameSaveReason reason)
    {
        for (int i = 0, count = this.listeners.Length; i < count; i++)
        {
            var listener = this.listeners[i];
            listener.OnSaveGame(reason);
        }

        this.remainingSeconds = SAVE_PERIOD_IN_SECONDS;
    }
}