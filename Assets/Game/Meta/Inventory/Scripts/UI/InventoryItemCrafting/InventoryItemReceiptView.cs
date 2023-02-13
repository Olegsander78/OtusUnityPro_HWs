using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public sealed class InventoryItemReceiptView : MonoBehaviour
{
    public event UnityAction OnCraftButtonClicked
    {
        add { this.craftButton.AddListener(value); }
        remove { this.craftButton.RemoveListener(value); }
    }

    public int IngredientCount
    {
        get { return this.ingredients.Length; }
    }

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private CraftButton craftButton;

    [Space]
    [SerializeField]
    private InventoryItemIngredientView[] ingredients;

    public void SetTitle(string title)
    {
        this.titleText.text = title;
    }

    public void SetDescription(string description)
    {
        this.descriptionText.text = description;
    }

    public void SetInteractibleButton(bool interactible)
    {
        var state = interactible
            ? CraftButton.State.AVAILABLE
            : CraftButton.State.LOCKED;
        this.craftButton.SetState(state);
    }

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }

    public InventoryItemIngredientView GetIngredient(int index)
    {
        return this.ingredients[index];
    }
}