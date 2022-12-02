using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ProductPopup : Popup
{
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private BuyButton buyButton;

    private IPresentationModel presenter;

    protected override void OnShow(object args)
    {
        if (args is not IPresentationModel presenter)
        {
            throw new Exception("Expected Presentation model!");
        }

        this.presenter = presenter;
        this.presenter.OnBuyButtonStateChanged += this.OnBuyButtonStateChanged;
        this.presenter.Start();

        this.titleText.text = presenter.GetTitle();
        this.descriptionText.text = presenter.GetDescription();
        this.iconImage.sprite = presenter.GetIcon();

        this.buyButton.SetPrice(presenter.GetPrice());
        this.buyButton.SetAvailable(presenter.CanBuy());
        this.buyButton.AddListener(this.OnBuyButtonClicked);
    }

    protected override void OnHide()
    {
        this.buyButton.RemoveListener(this.OnBuyButtonClicked);
        this.presenter.OnBuyButtonStateChanged -= this.OnBuyButtonStateChanged;
        this.presenter.Stop();
    }

    private void OnBuyButtonStateChanged(bool isAvailabe)
    {
        this.buyButton.SetAvailable(isAvailabe);
    }

    private void OnBuyButtonClicked()
    {
        this.presenter.OnBuyClicked();
    }

    public interface IPresentationModel
    {
        event Action<bool> OnBuyButtonStateChanged;

        void Start();

        void Stop();

        string GetTitle();

        string GetDescription();

        Sprite GetIcon();

        string GetPrice();

        bool CanBuy();

        void OnBuyClicked();
    }
}