using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class ChestView : MonoBehaviour
{
    public MissionProgressBar ProgressBar
    {
        get { return _progressBar; }
        set { _progressBar = value; }
    }

    public ChestRewardButton RewardButton
    {
        get { return _buttonReward; }
    }
    public BuyButton OpenNowButton
    {
        get { return _buttonOpenNow; }
    }

    [SerializeField]
    private Image _iconImage;

    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private TextMeshProUGUI _description;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private MissionProgressBar _progressBar;

    [SerializeField]
    private ChestRewardButton _buttonReward;

    [SerializeField]
    private BuyButton _buttonOpenNow;

    public void SetIcon(Sprite icon)
    {
        _iconImage.sprite = icon;
    }

    public void SetTitle(string title)
    {
        _titleText.text = title;
    }

    public void SetDescrition(string description)
    {
        _description.text = description;
    }

    public void SetRemainingTime(float remainingSeconds, float durationSeconds)
    {
        var progress = remainingSeconds / durationSeconds;        

        var timeSpan = TimeSpan.FromSeconds(remainingSeconds);
        _timerText.text = string.Format("{0:D1}h:{1:D2}m:{2:D2}s",
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds
        );

        _progressBar.SetProgress(progress, _timerText.text);
    }
}