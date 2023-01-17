using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public sealed class UpgradeView : MonoBehaviour
{
    public UpgradeButton UpgradeButton
    {
        get { return this.upgradeButton; }
    }

    [SerializeField]
    private Image iconImage;

    [Space]
    [SerializeField]
    private TextMeshProUGUI titleText;

    [FormerlySerializedAs("valueText")]
    [SerializeField]
    private TextMeshProUGUI statsText;

    [SerializeField]
    private TextMeshProUGUI levelText;

    [Space]
    [SerializeField]
    private UpgradeButton upgradeButton;

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }

    public void SetTitle(string title)
    {
        this.titleText.text = title;
    }

    public void SetStats(string stats)
    {
        this.statsText.text = stats;
    }

    public void SetLevel(string level)
    {
        this.levelText.text = level;
    }
}