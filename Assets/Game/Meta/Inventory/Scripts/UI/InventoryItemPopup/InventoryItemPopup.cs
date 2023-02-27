using System;
using TMPro;
using UIFrames.Unity;
using UnityEngine;
using UnityEngine.UI;


public sealed class InventoryItemPopup : UnityFrame
{
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI decriptionText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private Button consumeButton;

    [SerializeField]
    private Button equipButton;

    [SerializeField]
    private Button unequipButton;

    [Space]
    [SerializeField]
    private StackView stackView;

    private IPresenter presenter;

    private void OnEnable()
    {
        this.consumeButton.onClick.AddListener(this.OnConsumeButtonClicked);
        this.equipButton.onClick.AddListener(this.OnEquipButtonClicked);
        this.unequipButton.onClick.AddListener(this.OnUnequipButtonClicked);
    }


    private void OnDisable()
    {
        this.consumeButton.onClick.RemoveListener(this.OnConsumeButtonClicked);
        this.equipButton.onClick.RemoveListener(this.OnEquipButtonClicked);
        this.unequipButton.onClick.RemoveListener(this.OnUnequipButtonClicked);
    }

    protected override void OnShow(object args)
    {
        if (args is not IPresenter presenter)
        {
            throw new Exception("Expected Presenter!");
        }

        this.titleText.text = presenter.Title;
        this.decriptionText.text = presenter.Description;
        this.iconImage.sprite = presenter.Icon;

        this.presenter = presenter;

        this.SetupButton(presenter);
        
        //this.SetupConsumeButton(presenter);

        this.SetupStackContainer(presenter);              
    }

    private void OnConsumeButtonClicked()
    {
        this.presenter.OnConsumeClicked();
    }

    private void OnEquipButtonClicked()
    {
        this.presenter.OnEquipClicked();
    }

    private void OnUnequipButtonClicked()
    {
        this.presenter.OnUnequipClicked();
    }


    private void SetupStackContainer(IPresenter presenter)
    {
        var isStackableItem = presenter.IsStackableItem();
        this.stackView.SetVisible(isStackableItem);

        if (isStackableItem)
        {
            presenter.GetStackInfo(out var current, out var size);
            this.stackView.SetAmount(current, size);
        }
    }

    private void SetupButton(IPresenter presenter)
    {
        var isConsumableItem = presenter.IsConsumableItem();
        var isEquipableItem = presenter.IsEquipableItem();
        var isEquipped = presenter.IsEquippedItem();

        //if (isConsumableItem)
        //{
        //    SetupConsumeButton(presenter);
        //    return;
        //}
        if (isEquipableItem)
        {
            if (isEquipped)
            {
                SetupUnequipButton();
                Debug.Log("Button Unquip Item setuped!");
            }
            else
            {
                SetupEquipButton();
                Debug.Log("Button Equip Item setuped!");
            }
        }
        else
        {
            SetupConsumeButton();
            Debug.Log("Button Consume Item setuped!");
        }
    }

    private void SetupConsumeButton()
    {
        //var isConsumableItem = presenter.IsConsumableItem();
        //this.consumeButton.gameObject.SetActive(isConsumableItem);
        //if (isConsumableItem)
        //{
        //    this.equipButton.gameObject.SetActive(false);
        //    this.unequipButton.gameObject.SetActive(false);
        //    this.consumeButton.interactable = presenter.CanConsumeItem();
        //}
        this.consumeButton.gameObject.SetActive(true);
        this.equipButton.gameObject.SetActive(false);
        this.unequipButton.gameObject.SetActive(false);
        this.consumeButton.interactable = true;
    }

    private void SetupEquipButton()
    {
        this.consumeButton.gameObject.SetActive(false);
        this.equipButton.gameObject.SetActive(true);
        this.unequipButton.gameObject.SetActive(false);
        this.equipButton.interactable = true;
    }

    private void SetupUnequipButton()
    {
        this.consumeButton.gameObject.SetActive(false);
        this.equipButton.gameObject.SetActive(false);
        this.unequipButton.gameObject.SetActive(true);
        this.unequipButton.interactable = true;
    }

    public interface IPresenter
    {
        string Title { get; }

        string Description { get; }

        Sprite Icon { get; }

        bool IsStackableItem();

        void GetStackInfo(out int current, out int size);

        bool IsConsumableItem();

        bool CanConsumeItem();

        void OnConsumeClicked();
        void OnEquipClicked();
        bool IsEquipableItem();
        bool CanEquipItem();
        void OnUnequipClicked();
        bool IsEquippedItem();
    }
}