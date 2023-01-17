using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public sealed class UpgradeView : MonoBehaviour
{
    public UpgradeButton UpgradeButton
    {
        get { return _upgradeButton; }
    }

    [SerializeField]
    private Image _iconImage;

    [Space]
    [SerializeField]
    private TextMeshProUGUI _titleText;

    [FormerlySerializedAs("valueText")]
    [SerializeField]
    private TextMeshProUGUI _statsText;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [Space]
    [SerializeField]
    private UpgradeButton _upgradeButton;

    public void SetIcon(Sprite icon)
    {
        _iconImage.sprite = icon;
    }

    public void SetTitle(string title)
    {
        _titleText.text = title;
    }

    public void SetStats(string stats)
    {
        _statsText.text = stats;
    }

    public void SetLevel(string level)
    {
        _levelText.text = level;
    }
}