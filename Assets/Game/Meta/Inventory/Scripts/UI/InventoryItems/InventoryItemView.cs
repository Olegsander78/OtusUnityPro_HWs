using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public sealed class InventoryItemView : MonoBehaviour
{
    public StackView Stack
    {
        get { return this.stackView; }
    }

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private Button button;

    [SerializeField]
    private StackView stackView;

    public void SetTitle(string title)
    {
        this.titleText.text = title;
    }

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }

    public void AddClickListener(UnityAction action)
    {
        this.button.onClick.AddListener(action);
    }

    public void RemoveClickListener(UnityAction action)
    {
        this.button.onClick.RemoveListener(action);
    }
}