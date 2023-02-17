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

    [Space]
    [SerializeField]
    private StackView stackView;

    private IPresenter presenter;

    private void OnEnable()
    {
        this.consumeButton.onClick.AddListener(this.OnConsumeButtonClicked);
        this.equipButton.onClick.AddListener(this.OnEquipButtonClicked);
    }

    private void OnDisable()
    {
        this.consumeButton.onClick.RemoveListener(this.OnConsumeButtonClicked);
        this.equipButton.onClick.RemoveListener(this.OnEquipButtonClicked);
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

        this.SetupStackContainer(presenter);
        //this.SetupEquipButton(presenter);
        //this.SetupConsumeButton(presenter);
        this.SetupButtons(presenter);

        this.presenter = presenter;
    }

    private void OnConsumeButtonClicked()
    {
        this.presenter.OnConsumeClicked();
    }

    private void OnEquipButtonClicked()
    {
        this.presenter.OnEquipClicked();
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

    private void SetupButtons(IPresenter presenter)
    {
        var isConsumableItem = presenter.IsConsumableItem();
        var isEquipableItem = presenter.IsEquipableItem();

        if (isConsumableItem)
        {
            SetupConsumeButton();
        }
        else if (isEquipableItem)
        {
            SetupEquipButton();
        }
    }

    private void SetupConsumeButton()
    {
        this.consumeButton.gameObject.SetActive(true);
        this.equipButton.gameObject.SetActive(false);
        this.consumeButton.interactable = true;

        //var isConsumableItem = presenter.IsConsumableItem();
        //this.consumeButton.gameObject.SetActive(isConsumableItem);
        //this.equipButton.gameObject.SetActive(false);
        //if (isConsumableItem)
        //{
        //    this.consumeButton.interactable = presenter.CanConsumeItem();
        //}
    }

    private void SetupEquipButton()
    {
        this.consumeButton.gameObject.SetActive(false);
        this.equipButton.gameObject.SetActive(true);
        this.equipButton.interactable = true;

        //var isEquipableItem = presenter.IsEquipableItem();
        //this.equipButton.gameObject.SetActive(isEquipableItem);
        //this.consumeButton.gameObject.SetActive(false);
        //if (isEquipableItem)
        //{
        //    this.equipButton.interactable = presenter.CanEquipItem();
        //}
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
    }
}