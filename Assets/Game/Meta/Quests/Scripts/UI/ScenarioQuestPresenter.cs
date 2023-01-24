using System;
//using Game.Localization;
using UnityEngine;
using static Unity.VisualScripting.Icons;


[Serializable]
public sealed class ScenarioQuestPresenter
{
    public bool IsShown
    {
        get { return _scenarioQuest != null; }
    }

    [SerializeField]
    private ScenarioQuestView _view;

    private ScenarioQuest _scenarioQuest;

    private ScenarioQuestManager _scenarioQuestManager;

    //private MoneyPanelAnimator_AddMoney moneyPanelAnimator;

    public void Construct(ScenarioQuestManager scenarioManager)
       // MoneyPanelAnimator_AddMoney moneyPanelAnimator    
    {
        _scenarioQuestManager = scenarioManager;
        //this.moneyPanelAnimator = moneyPanelAnimator;
    }

    public void Start(ScenarioQuest quest)
    {
        if (_scenarioQuest != null)
        {
            throw new Exception("Scenario Quest presenter is already shown!");
        }

        _scenarioQuest = quest;

        _view.SetIcon(_scenarioQuest.Metadata.icon);
        _view.gameObject.SetActive(true);

        SetupProgressBar();
        SetupRewardButton();

       _view.RewardButton.AddListener(this.OnButtonClicked);
        _scenarioQuest.OnProgressChanged += this.OnScenarioQuestProgressChanged;
        _scenarioQuest.OnCompleted += this.OnScenarioQuestCompleted;

        OnUpdateTitle();

        //LanguageManager.OnLanguageChanged += this.OnUpdateLanguage;
        //this.OnUpdateLanguage(LanguageManager.CurrentLanguage);
    }

    public void Stop()
    {
        _view.RewardButton.RemoveListener(this.OnButtonClicked);
        _view.gameObject.SetActive(false);

        if (_scenarioQuest != null)
        {
            _scenarioQuest.OnProgressChanged -= this.OnScenarioQuestProgressChanged;
            _scenarioQuest.OnCompleted -= this.OnScenarioQuestCompleted;
            _scenarioQuest = null;
        }

        //LanguageManager.OnLanguageChanged -= this.OnUpdateLanguage;
    }

    private void OnButtonClicked()
    {
        var quest = _scenarioQuest;
        if (_scenarioQuestManager.CanReceiveReward(quest))
        {
            _scenarioQuestManager.ReceiveReward(quest);
            AnimateIncome(quest);
        }
    }

    private void OnScenarioQuestProgressChanged(ScenarioQuest quest)
    {
        var progress = quest.NormalizedProgress;
        var text = quest.TextProgress;
        _view.ProgressBar.SetProgress(progress, text);
    }

    private void OnScenarioQuestCompleted(ScenarioQuest quest)
    {
        _view.RewardButton.SetState(MissionRewardButton.State.COMPLETE);

        var completedQuestText = quest.Metadata.storyScenarioQuestStageOnCompleted;
        _view.SetScenarioQuestText(completedQuestText);
    }

    private void OnUpdateTitle()
    {
        var title = _scenarioQuest.Metadata.localizedTitle;
        _view.SetTitle(title);

        var startQuestText = _scenarioQuest.Metadata.storyScenarioQuestStage;
        _view.SetScenarioQuestText(startQuestText);

        var stageKey = _scenarioQuest.StageScenarioQuest;
        var stageText = stageKey.ToString();
        _view.SetStage(stageText);
    }

    private void SetupRewardButton()
    {
        var button = _view.RewardButton;

        var reward = _scenarioQuest.MoneyReward.ToString();
        var expReward = _scenarioQuest.ExpReward.ToString();
        button.SetMoneyReward(reward);
        button.SetExpReward(expReward);

        var state = _scenarioQuest.State == QuestState.COMPLETED
            ? MissionRewardButton.State.COMPLETE
            : MissionRewardButton.State.PROCESSING;
        button.SetState(state);
    }

    private void SetupProgressBar()
    {
        var progress = _scenarioQuest.NormalizedProgress;
        var text = _scenarioQuest.TextProgress;
        _view.ProgressBar.SetProgress(progress, text);
    }

    private void AnimateIncome(ScenarioQuest quest)
    {
        var rectTransform = _view.RewardButton.GetComponent<RectTransform>();
        var startUIPosition = rectTransform.TransformPoint(rectTransform.rect.center);
        var reward = quest.MoneyReward;
        //this.moneyPanelAnimator.PlayIncomeFromUI(startUIPosition, reward);
    }



    //private void OnUpdateLanguage(SystemLanguage language)
    //{
    //    var title = LocalizationManager.GetText(this.mission.Metadata.localizedTitle, language);
    //    this.view.SetTitle(title);

    //    var difficultyKey = MissionExtensions.GetDifficultyLocalizationKey(this.mission.Difficulty);
    //    //var difficultyText = LocalizationManager.GetText(difficultyKey, language);
    //    this.view.SetDifficulty(difficultyText);
    //}
}