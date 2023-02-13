using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class InventoryItemIngredientView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;

    [Space]
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private Sprite completedIcon;

    [SerializeField]
    private Sprite uncompletedIcon;

    public void Setup(string title, int requiredCount, int actualCount)
    {
        this.titleText.text = $"{title}: {actualCount}/{requiredCount}";
        this.iconImage.sprite = actualCount >= requiredCount
            ? this.completedIcon
            : this.uncompletedIcon;
    }

    public void SetVisible(bool isVisible)
    {
        this.gameObject.SetActive(isVisible);
    }
}