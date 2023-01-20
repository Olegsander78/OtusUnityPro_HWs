using System;
//using Game.Localization;
using UnityEngine;


[Serializable]
public sealed class MissionPresenter
{
    public bool IsShown
    {
        get { return this.mission != null; }
    }

    [SerializeField]
    private MissionView view;

    private Mission mission;

    private MissionsManager missionsManager;

    //private MoneyPanelAnimator_AddMoney moneyPanelAnimator;

    public void Construct(
        MissionsManager missionsManager
       // MoneyPanelAnimator_AddMoney moneyPanelAnimator
    )
    {
        this.missionsManager = missionsManager;
        //this.moneyPanelAnimator = moneyPanelAnimator;
    }

    public void Start(Mission mission)
    {
        if (this.mission != null)
        {
            throw new Exception("Mission presenter is already shown!");
        }

        this.mission = mission;

        this.view.SetIcon(this.mission.Metadata.icon);
        this.view.gameObject.SetActive(true);

        this.SetupProgressBar();
        this.SetupRewardButton();

        this.view.RewardButton.AddListener(this.OnButtonClicked);
        this.mission.OnProgressChanged += this.OnMissionProgressChanged;
        this.mission.OnCompleted += this.OnMissionCompleted;

        //LanguageManager.OnLanguageChanged += this.OnUpdateLanguage;
        //this.OnUpdateLanguage(LanguageManager.CurrentLanguage);
    }

    public void Stop()
    {
        this.view.RewardButton.RemoveListener(this.OnButtonClicked);
        this.view.gameObject.SetActive(false);

        if (this.mission != null)
        {
            this.mission.OnProgressChanged -= this.OnMissionProgressChanged;
            this.mission.OnCompleted -= this.OnMissionCompleted;
            this.mission = null;
        }

        //LanguageManager.OnLanguageChanged -= this.OnUpdateLanguage;
    }

    #region UIEvents

    private void OnButtonClicked()
    {
        var mission = this.mission;
        if (this.missionsManager.CanReceiveReward(mission))
        {
            this.missionsManager.ReceiveReward(mission);
            this.AnimateIncome(mission);
        }
    }

    #endregion

    #region ModelEvents

    private void OnMissionProgressChanged(Mission mission)
    {
        var progress = mission.NormalizedProgress;
        var text = mission.TextProgress;
        this.view.ProgressBar.SetProgress(progress, text);
    }

    private void OnMissionCompleted(Mission mission)
    {
        this.view.RewardButton.SetState(MissionRewardButton.State.COMPLETE);
    }

    private void OnUpdateLanguage(SystemLanguage language)
    {
        //var title = LocalizationManager.GetText(this.mission.Metadata.localizedTitle, language);
        //this.view.SetTitle(title);

        //var difficultyKey = MissionExtensions.GetDifficultyLocalizationKey(this.mission.Difficulty);
        ////var difficultyText = LocalizationManager.GetText(difficultyKey, language);
        //this.view.SetDifficulty(difficultyText);
    }

    #endregion

    private void SetupRewardButton()
    {
        var button = this.view.RewardButton;

        var reward = this.mission.MoneyReward.ToString();
        button.SetReward(reward);

        var state = this.mission.State == MissionState.COMPLETED
            ? MissionRewardButton.State.COMPLETE
            : MissionRewardButton.State.PROCESSING;
        button.SetState(state);
    }

    private void SetupProgressBar()
    {
        var progress = this.mission.NormalizedProgress;
        var text = this.mission.TextProgress;
        this.view.ProgressBar.SetProgress(progress, text);
    }

    private void AnimateIncome(Mission mission)
    {
        var rectTransform = this.view.RewardButton.GetComponent<RectTransform>();
        var startUIPosition = rectTransform.TransformPoint(rectTransform.rect.center);
        var reward = mission.MoneyReward;
        //this.moneyPanelAnimator.PlayIncomeFromUI(startUIPosition, reward);
    }
}