using System;
using Sirenix.OdinInspector;


[Serializable]
public abstract class DailyQuest
{
    public event Action<DailyQuest> OnStarted;

    public abstract event Action<DailyQuest> OnProgressChanged;

    public event Action<DailyQuest> OnCompleted;

    [ReadOnly]
    [ShowInInspector]
    public string Id
    {
        get { return this.config.Id; }
    }

    [ReadOnly]
    [ShowInInspector]
    public DailyQuestState State { get; private set; }

    [ReadOnly]
    [ShowInInspector]
    public DailyQuestDifficulty Difficulty
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
    public DailyQuestMetadata Metadata
    {
        get { return this.config.Metadata; }
    }

    [ReadOnly]
    [ShowInInspector]
    public abstract float NormalizedProgress { get; }

    [ReadOnly]
    [ShowInInspector]
    public abstract string TextProgress { get; }

    private readonly DailyQuestConfig config;

    public DailyQuest(DailyQuestConfig config)
    {
        this.config = config;
        this.State = DailyQuestState.NOT_STARTED;
    }

    public void Start()
    {
        if (this.State == DailyQuestState.STARTED)
        {
            throw new Exception("DailyQuest is already started!");
        }

        this.State = DailyQuestState.STARTED;
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
        if (this.State != DailyQuestState.STARTED)
        {
            return;
        }

        this.State = DailyQuestState.NOT_STARTED;
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
        if (this.State != DailyQuestState.STARTED)
        {
            throw new Exception("DailyQuest is not started!");
        }

        this.State = DailyQuestState.COMPLETED;
        this.OnStop();
        this.OnCompleted?.Invoke(this);
    }
}