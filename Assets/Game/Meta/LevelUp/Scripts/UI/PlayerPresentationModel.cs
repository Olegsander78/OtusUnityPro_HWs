using System;
using UnityEngine;


public sealed class PlayerPresentationModel : PlayerPopup.IPresentationModel
{
    public event Action<bool> OnBuyButtonStateChanged;

    private readonly Product product;

    private readonly ProductBuyer productBuyer;

    private readonly MoneyStorage moneyStorage;

    public PlayerPresentationModel(Product product, ProductBuyer productBuyer, MoneyStorage moneyStorage)
    {
        this.product = product;
        this.productBuyer = productBuyer;
        this.moneyStorage = moneyStorage;
    }

    public void Start()
    {
        this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
    }

    public void Stop()
    {
        this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
    }

    public string GetTitle()
    {
        return this.product.title;
    }

    public string GetDescription()
    {
        return this.product.description;
    }

    public Sprite GetIcon()
    {
        return this.product.icon;
    }

    public string GetPrice()
    {
        return this.product.price.ToString();
    }

    public bool CanBuy()
    {
        return this.productBuyer.CanBuy(this.product);
    }

    public void OnBuyClicked()
    {
        this.productBuyer.Buy(this.product);
    }

    private void OnMoneyChanged(int money)
    {
        var canBuy = this.productBuyer.CanBuy(this.product);
        this.OnBuyButtonStateChanged?.Invoke(canBuy);
    }
}