using System;
using Sirenix.OdinInspector;


[Serializable]
public abstract class ScenarioQuest
{
    public event Action<ScenarioQuest> OnStarted;

    public abstract event Action<ScenarioQuest> OnProgressChanged;

    public event Action<ScenarioQuest> OnCompleted;

    [ReadOnly]
    [ShowInInspector]
    public string Id
    {
        get { return _config.Id; }
    }

    [ReadOnly]
    [ShowInInspector]
    public QuestState State { get; private set; }

    [ReadOnly]
    [ShowInInspector]
    public ScenarioQuestStage StageScenarioQuest
    {
        get { return _config.ScenarioQuestStage; }
    }

    [ReadOnly]
    [ShowInInspector]
    public int MoneyReward
    {
        get { return _config.MoneyReward; }
    }

    [ReadOnly]
    [ShowInInspector]
    public int ExpReward
    {
        get { return _config.ExpReward; }
    }

    [ReadOnly]
    [ShowInInspector]
    public ScenarioQuestMetadata Metadata
    {
        get { return _config.Metadata; }
    }

    [ReadOnly]
    [ShowInInspector]
    public abstract float NormalizedProgress { get; }

    [ReadOnly]
    [ShowInInspector]
    public abstract string TextProgress { get; }

    private readonly ScenarioQuestConfig _config;

    public ScenarioQuest(ScenarioQuestConfig config)
    {
        _config = config;
        State = QuestState.NOT_STARTED;
    }

    public void Start()
    {
        if (State == QuestState.STARTED)
        {
            throw new Exception("ScenarioQuest is already started!");
        }

        State = QuestState.STARTED;
        OnStarted?.Invoke(this);

        if (NormalizedProgress >= 1.0f)
        {
            Complete();
            return;
        }

        OnStart();
    }

    public void Stop()
    {
        if (State != QuestState.STARTED)
        {
            return;
        }

        State = QuestState.NOT_STARTED;
        OnStop();
    }

    #region Callbacks

    protected abstract void OnStart();

    protected abstract void OnStop();

    #endregion

    protected void TryComplete()
    {
        if (NormalizedProgress >= 1.0f)
        {
            Complete();
        }
    }

    private void Complete()
    {
        if (State != QuestState.STARTED)
        {
            throw new Exception("ScenarioQuest is not started!");
        }

        State = QuestState.COMPLETED;
        OnStop();
        OnCompleted?.Invoke(this);
    }
}