using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class PricePanel : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private TextMeshProUGUI priceText;

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }

    public void SetPrice(string price)
    {
        this.priceText.text = price;
    }
}