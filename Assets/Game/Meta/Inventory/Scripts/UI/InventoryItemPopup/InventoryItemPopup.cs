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

    [Space]
    [SerializeField]
    private StackView stackView;

    private IPresenter presenter;

    private void OnEnable()
    {
        this.consumeButton.onClick.AddListener(this.OnConsumeButtonClicked);
    }

    private void OnDisable()
    {
        this.consumeButton.onClick.RemoveListener(this.OnConsumeButtonClicked);
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
        this.SetupConsumeButton(presenter);

        this.presenter = presenter;
    }

    private void OnConsumeButtonClicked()
    {
        this.presenter.OnConsumeClicked();
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

    private void SetupConsumeButton(IPresenter presenter)
    {
        var isConsumableItem = presenter.IsConsumableItem();
        this.consumeButton.gameObject.SetActive(isConsumableItem);
        if (isConsumableItem)
        {
            this.consumeButton.interactable = presenter.CanConsumeItem();
        }
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
    }
}