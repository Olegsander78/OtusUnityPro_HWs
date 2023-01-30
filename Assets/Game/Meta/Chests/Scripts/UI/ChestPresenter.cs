using System;
//using Game.Localization;
using UnityEngine;
using static Unity.VisualScripting.Icons;


[Serializable]
public sealed class ChestPresenter
{
    public bool IsShown
    {
        get { return _chest != null; }
    }

    [SerializeField]
    private ChestView _view;

    private Chest _chest;

    private ChestsManager _chestsManager;

    //private MoneyPanelAnimator_AddMoney moneyPanelAnimator;

    public void Construct(ChestsManager chestsManager)
       // MoneyPanelAnimator_AddMoney moneyPanelAnimator    
    {
        _chestsManager = chestsManager;
        //this.moneyPanelAnimator = moneyPanelAnimator;
    }

    public void Start(Chest chest)
    {
        if (_chest != null)
        {
            throw new Exception("Chest Presenter is already shown!");
        }

        _chest = chest;

        _view.SetIcon(_chest.Config.ChestMetadata.icon);
        _view.gameObject.SetActive(true);

        SetupProgressBar();
        SetupRewardButton();

        _view.RewardButton.AddListener(OnButtonClicked);
        _chest.OnTimeChanged += OnChestProgressChanged;
        _chest.OnCompleted += OnChestCompleted;

        OnUpdateTitle();

        //LanguageManager.OnLanguageChanged += this.OnUpdateLanguage;
        //this.OnUpdateLanguage(LanguageManager.CurrentLanguage);
    }

    public void Stop()
    {
        _view.RewardButton.RemoveListener(OnButtonClicked);
        _view.gameObject.SetActive(false);

        if (_chest != null)
        {
            _chest.OnTimeChanged -= OnChestProgressChanged;
            _chest.OnCompleted -= OnChestCompleted;
            _chest = null;
        }

        //LanguageManager.OnLanguageChanged -= this.OnUpdateLanguage;
    }


    private void OnButtonClicked()
    {
        var chest = _chest;
        if (_chestsManager.CanReceiveReward(chest))
        {
            _chestsManager.ReceiveReward(chest);
            AnimateIncome(chest);

            SetupProgressBar();
            SetupRewardButton();
        }        
    }

    private void OnChestProgressChanged(Chest chest, float remainingSec)
    {
        var progress = remainingSec/chest.DurationSeconds;

        var timeSpan = TimeSpan.FromSeconds(remainingSec);
        var timerText = string.Format("{0:D1}h:{1:D2}m:{2:D2}s",
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds
        );

        _view.ProgressBar.SetProgress(progress, timerText);
    }

    private void OnChestCompleted(Chest chest)
    {
        _view.RewardButton.SetState(ChestRewardButton.State.COMPLETE);
    }

    private void OnUpdateTitle()
    {
        var title = _chest.Config.ChestMetadata.DisplayName;
        _view.SetTitle(title);

        var descrition = _chest.Config.ChestMetadata.Description;
        _view.SetDescrition(descrition);
    }


    private void SetupRewardButton()
    {

        if (!_chest.IsActive)
        {
            _view.RewardButton.SetState(ChestRewardButton.State.COMPLETE);
        }
        else
        {
            _view.RewardButton.SetState(ChestRewardButton.State.PROCESSING);
        }

    }

    private void SetupProgressBar()
    {
        float progress = _chest.NormalizedProgress;

        var timeSpan = TimeSpan.FromSeconds(_chest.RemainingSeconds);
        var timerText = string.Format("{0:D1}h:{1:D2}m:{2:D2}s",
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds
        );
        _view.ProgressBar.SetProgress(progress, timerText);
    }

    private void AnimateIncome(Chest chest)
    {
        var rectTransform = _view.RewardButton.GetComponent<RectTransform>();
        var startUIPosition = rectTransform.TransformPoint(rectTransform.rect.center);
        //var reward = chest.MoneyReward;

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