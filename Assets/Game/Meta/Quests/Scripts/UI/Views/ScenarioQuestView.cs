using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class ScenarioQuestView : MonoBehaviour
{
    public MissionProgressBar ProgressBar
    {
        get { return this._progressBar; }
    }

    public MissionRewardButton RewardButton
    {
        get { return this._button; }
    }

    [SerializeField]
    private Image _iconImage;

    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private TextMeshProUGUI _scenarioQuestText;

    [SerializeField]
    private TextMeshProUGUI _stageText;

    [SerializeField]
    private MissionProgressBar _progressBar;

    [SerializeField]
    private MissionRewardButton _button;

    public void SetIcon(Sprite icon)
    {
        _iconImage.sprite = icon;
    }

    public void SetTitle(string title)
    {
        _titleText.text = title;
    }

    public void SetScenarioQuestText(string startText)
    {
        _scenarioQuestText.text = startText;
    }

    public void SetStage(string stage)
    {
        _stageText.text = stage.ToUpper();
    }
}