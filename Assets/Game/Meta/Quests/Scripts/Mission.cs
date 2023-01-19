using System;
using Sirenix.OdinInspector;


[Serializable]
public abstract class Mission
{
    public event Action<Mission> OnStarted;

    public abstract event Action<Mission> OnProgressChanged;

    public event Action<Mission> OnCompleted;

    [ReadOnly]
    [ShowInInspector]
    public string Id
    {
        get { return this.config.Id; }
    }

    [ReadOnly]
    [ShowInInspector]
    public MissionState State { get; private set; }

    [ReadOnly]
    [ShowInInspector]
    public MissionDifficulty Difficulty
    {
        get { return this.config.Difficulty; }
    }

    [ReadOnly]
    [ShowInInspector]
    public int MoneyReward
    {
        get { return this.config.MoneyReward; }
    }

    [ReadOnly]
    [ShowInInspector]
    public int ExpReward
    {
        get { return this.config.ExpReward; }
    }

    [ReadOnly]
    [ShowInInspector]
    public MissionMetadata Metadata
    {
        get { return this.config.Metadata; }
    }

    [ReadOnly]
    [ShowInInspector]
    public abstract float NormalizedProgress { get; }

    [ReadOnly]
    [ShowInInspector]
    public abstract string TextProgress { get; }

    private readonly MissionConfig config;

    public Mission(MissionConfig config)
    {
        this.config = config;
        this.State = MissionState.NOT_STARTED;
    }

    public void Start()
    {
        if (this.State == MissionState.STARTED)
        {
            throw new Exception("Mission is already started!");
        }

        this.State = MissionState.STARTED;
        this.OnStarted?.Invoke(this);

        if (this.NormalizedProgress >= 1.0f)
        {
            this.Complete();
            return;
        }

        this.OnStart();
    }

    public void Stop()
    {
        if (this.State != MissionState.STARTED)
        {
            return;
        }

        this.State = MissionState.NOT_STARTED;
        this.OnStop();
    }

    #region Callbacks

    protected abstract void OnStart();

    protected abstract void OnStop();

    #endregion

    protected void TryComplete()
    {
        if (this.NormalizedProgress >= 1.0f)
        {
            this.Complete();
        }
    }

    private void Complete()
    {
        if (this.State != MissionState.STARTED)
        {
            throw new Exception("Mission is not started!");
        }

        this.State = MissionState.COMPLETED;
        this.OnStop();
        this.OnCompleted?.Invoke(this);
    }
}