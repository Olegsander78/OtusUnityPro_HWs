using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class ProductDialog : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private Image currencyIconImage;

    [SerializeField]
    private Image productIconImage;

    public void Show()
    {
        this.root.SetActive(true);
    }

    public void Hide()
    {
        this.root.SetActive(false);
    }

    public void SetPrice(string price)
    {
        this.priceText.text = price;
    }

    public void SetProductIcon(Sprite icon)
    {
        this.productIconImage.sprite = icon;
    }

    public void SetCurrencyIcon(Sprite icon)
    {
        this.currencyIconImage.sprite = icon;
    }
}