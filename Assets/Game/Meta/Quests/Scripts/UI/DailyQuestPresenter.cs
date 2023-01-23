using System;
//using Game.Localization;
using UnityEngine;
using static Unity.VisualScripting.Icons;


[Serializable]
public sealed class DailyQuestPresenter
{
    public bool IsShown
    {
        get { return this.dailyQuest != null; }
    }

    [SerializeField]
    private DailyQuestView view;

    private DailyQuest dailyQuest;

    private DailyQuestManager dailyQuestManager;

    //private MoneyPanelAnimator_AddMoney moneyPanelAnimator;

    public void Construct(DailyQuestManager missionsManager)
       // MoneyPanelAnimator_AddMoney moneyPanelAnimator    
    {
        this.dailyQuestManager = missionsManager;
        //this.moneyPanelAnimator = moneyPanelAnimator;
    }

    public void Start(DailyQuest mission)
    {
        if (this.dailyQuest != null)
        {
            throw new Exception("DailyQuest presenter is already shown!");
        }

        this.dailyQuest = mission;

        this.view.SetIcon(this.dailyQuest.Metadata.icon);
        this.view.gameObject.SetActive(true);

        this.SetupProgressBar();
        this.SetupRewardButton();

        this.view.RewardButton.AddListener(this.OnButtonClicked);
        this.dailyQuest.OnProgressChanged += this.OnDailyQuestProgressChanged;
        this.dailyQuest.OnCompleted += this.OnDailyQuestCompleted;

        OnUpdateTitle();

        //LanguageManager.OnLanguageChanged += this.OnUpdateLanguage;
        //this.OnUpdateLanguage(LanguageManager.CurrentLanguage);
    }

    public void Stop()
    {
        this.view.RewardButton.RemoveListener(this.OnButtonClicked);
        this.view.gameObject.SetActive(false);

        if (this.dailyQuest != null)
        {
            this.dailyQuest.OnProgressChanged -= this.OnDailyQuestProgressChanged;
            this.dailyQuest.OnCompleted -= this.OnDailyQuestCompleted;
            this.dailyQuest = null;
        }

        //LanguageManager.OnLanguageChanged -= this.OnUpdateLanguage;
    }

    #region UIEvents

    private void OnButtonClicked()
    {
        var mission = this.dailyQuest;
        if (this.dailyQuestManager.CanReceiveReward(mission))
        {
            this.dailyQuestManager.ReceiveReward(mission);
            this.AnimateIncome(mission);
        }
    }

    #endregion

    #region ModelEvents

    private void OnDailyQuestProgressChanged(DailyQuest mission)
    {
        var progress = mission.NormalizedProgress;
        var text = mission.TextProgress;
        this.view.ProgressBar.SetProgress(progress, text);
    }

    private void OnDailyQuestCompleted(DailyQuest mission)
    {
        this.view.RewardButton.SetState(MissionRewardButton.State.COMPLETE);
    }

    private void OnUpdateTitle()
    {
        var title = this.dailyQuest.Metadata.localizedTitle;
        this.view.SetTitle(title);

        var difficultyKey = this.dailyQuest.Difficulty;
        var difficultyText = difficultyKey.ToString();
        this.view.SetDifficulty(difficultyText);
    }

    #endregion

    private void SetupRewardButton()
    {
        var button = this.view.RewardButton;

        var reward = this.dailyQuest.MoneyReward.ToString();
        var expReward = dailyQuest.ExpReward.ToString();
        button.SetMoneyReward(reward);
        button.SetExpReward(expReward);

        var state = this.dailyQuest.State == QuestState.COMPLETED
            ? MissionRewardButton.State.COMPLETE
            : MissionRewardButton.State.PROCESSING;
        button.SetState(state);
    }

    private void SetupProgressBar()
    {
        var progress = this.dailyQuest.NormalizedProgress;
        var text = this.dailyQuest.TextProgress;
        this.view.ProgressBar.SetProgress(progress, text);
    }

    private void AnimateIncome(DailyQuest mission)
    {
        var rectTransform = this.view.RewardButton.GetComponent<RectTransform>();
        var startUIPosition = rectTransform.TransformPoint(rectTransform.rect.center);
        var reward = mission.MoneyReward;
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