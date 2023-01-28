using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class ChestView : MonoBehaviour
{
    public ChestProgressBar ProgressBar
    {
        get { return _progressBar; }
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
    private ChestProgressBar _progressBar;

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
}