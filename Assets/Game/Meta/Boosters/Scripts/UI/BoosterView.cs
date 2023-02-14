using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class BoosterView : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI labelText;

    [SerializeField]
    private ProgressBar progressBar;

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }

    public void SetColor(Color color)
    {
        this.progressBar.SetColor(color);
    }

    public void SetLabel(string label)
    {
        this.labelText.text = label;
    }

    public void SetRemainingTime(float remainingSeconds, float durationSeconds)
    {
        var progress = remainingSeconds / durationSeconds;
        this.progressBar.SetProgress(progress);

        var timeSpan = TimeSpan.FromSeconds(remainingSeconds);
        this.timerText.text = string.Format("{0:D1}h:{1:D2}m:{2:D2}s",
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds
        );
    }
}