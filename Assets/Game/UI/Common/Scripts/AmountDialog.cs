using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class AmountDialog : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private GameObject root;

    public void SetAmount(string amount)
    {
        this.amountText.text = amount;
    }

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }

    public void Show()
    {
        this.root.SetActive(true);
    }

    public void Hide()
    {
        this.root.SetActive(false);
    }
}