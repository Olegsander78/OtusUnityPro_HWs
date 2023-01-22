using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class DailyQuestView : MonoBehaviour
{
    public MissionProgressBar ProgressBar
    {
        get { return this.progressBar; }
    }

    public MissionRewardButton RewardButton
    {
        get { return this.button; }
    }

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI difficultyText;

    [SerializeField]
    private MissionProgressBar progressBar;

    [SerializeField]
    private MissionRewardButton button;

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }

    public void SetTitle(string title)
    {
        this.titleText.text = title;
    }

    public void SetDifficulty(string difficulty)
    {
        this.difficultyText.text = difficulty.ToUpper();
    }
}