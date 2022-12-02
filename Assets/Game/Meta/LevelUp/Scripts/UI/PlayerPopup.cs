using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class PlayerPopup : Popup
{
    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private TextMeshProUGUI _nameHeroText;

    [SerializeField]
    private TextMeshProUGUI _classHeroText;

    [SerializeField]
    private TextMeshProUGUI _historyHeroText;

    [SerializeField]
    private Image iconHeroImage;

    [SerializeField]
    private TextMeshProUGUI _levelHeroText;

    [SerializeField]
    private TextMeshProUGUI _hitPointsHeroText;

    [SerializeField]
    private TextMeshProUGUI _meleeDamageHeroText;

    [SerializeField]
    private LevelUpButton _levelUpButton;

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

        this._titleText.text = presenter.GetTitle();
        this._historyHeroText.text = presenter.GetDescription();
        this.iconHeroImage.sprite = presenter.GetIcon();

        //this._levelUpButton.SetPrice(presenter.GetPrice());
        this._levelUpButton.SetAvailable(presenter.CanBuy());
        this._levelUpButton.AddListener(this.OnBuyButtonClicked);
    }

    protected override void OnHide()
    {
        this._levelUpButton.RemoveListener(this.OnBuyButtonClicked);
        this.presenter.OnBuyButtonStateChanged -= this.OnBuyButtonStateChanged;
        this.presenter.Stop();
    }

    private void OnBuyButtonStateChanged(bool isAvailabe)
    {
        this._levelUpButton.SetAvailable(isAvailabe);
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