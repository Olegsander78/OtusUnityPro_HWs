using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class ProductView : MonoBehaviour
{
    public ProductBuyButton BuyButton
    {
        get { return this.button; }
    }

    [SerializeField]
    private ProductBuyButton button;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private Image iconImage;

    public void SetTitle(string title)
    {
        this.titleText.text = title;
    }

    public void SetDescription(string description)
    {
        this.descriptionText.text = description;
    }

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }
}