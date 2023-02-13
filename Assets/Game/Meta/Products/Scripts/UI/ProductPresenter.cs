using System;
using UnityEngine;


public sealed class ProductPresenter
{
    private readonly ProductView view;

    private ProductBuyer buyManager;

    private MoneyStorage moneyStorage;

    private ResourceStorage resourceStorage;

    private ResourceInfoCatalog resourceCatalog;

    private Sprite moneyIcon;

    private Product currentProduct;

    private bool isStarted;

    public ProductPresenter(ProductView view)
    {
        this.view = view;
    }

    public void Construct(
        ProductBuyer buyManager,
        MoneyStorage moneyStorage,
        ResourceStorage resourceStorage,
        ResourceInfoCatalog resourceCatalog,
        Sprite moneyIcon
    )
    {
        this.buyManager = buyManager;
        this.moneyStorage = moneyStorage;
        this.resourceStorage = resourceStorage;

        this.resourceCatalog = resourceCatalog;
        this.moneyIcon = moneyIcon;
    }

    public void SetProduct(Product product)
    {
        this.currentProduct = product;
        this.UpdateInfo();
        this.UpdatePrice();
        this.UpdateBuyButtonState();
    }

    public void Start()
    {
        if (this.isStarted)
        {
            return;
        }

        this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
        this.resourceStorage.OnResourceChanged += this.OnResourcesChanged;
        this.buyManager.OnCompleted += this.OnBuyCompleted;

        this.view.BuyButton.OnClicked += this.OnBuyButtonClicked;
        this.isStarted = true;

        this.UpdateBuyButtonState();
    }

    public void Stop()
    {
        if (!this.isStarted)
        {
            return;
        }

        this.buyManager.OnCompleted -= this.OnBuyCompleted;
        this.view.BuyButton.OnClicked -= this.OnBuyButtonClicked;
        this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
        this.resourceStorage.OnResourceChanged -= this.OnResourcesChanged;

        this.isStarted = false;
    }

    #region GameEvents

    private void OnBuyCompleted(Product product)
    {
        this.UpdateBuyButtonState();
    }

    private void OnResourcesChanged(ResourceType type, int amount)
    {
        this.UpdateBuyButtonState();
    }

    private void OnMoneyChanged(int money)
    {
        this.UpdateBuyButtonState();
    }

    #endregion

    #region UIEvents

    private void OnBuyButtonClicked()
    {
        if (this.buyManager.CanBuyProduct(this.currentProduct))
        {
            this.buyManager.BuyProduct(this.currentProduct);
        }
    }

    #endregion

    private void UpdateInfo()
    {
        var metadata = this.currentProduct.Metadata;
        this.view.SetTitle(metadata.Title);
        this.view.SetDescription(metadata.Decription);
        this.view.SetIcon(metadata.Icon);
    }

    private void UpdateBuyButtonState()
    {
        var canBuyProduct = this.buyManager.CanBuyProduct(this.currentProduct);
        var state = canBuyProduct
            ? ProductBuyButton.State.AVAILABLE
            : ProductBuyButton.State.LOCKED;
        this.view.BuyButton.SetState(state);
    }

    private void UpdatePrice()
    {
        if (this.currentProduct.TryGetComponent(out Component_MoneyPrice moneyPriceComponent))
        {
            this.SetMoneyPrice(moneyPriceComponent);
        }
        else if (this.currentProduct.TryGetComponent(out Component_ResourcePrice resourcePriceComponent))
        {
            this.SetResourcePrice(resourcePriceComponent);
        }
    }

    private void SetMoneyPrice(Component_MoneyPrice component)
    {
        var buyButton = this.view.BuyButton;
        buyButton.SetPriceSize1();

        var pricePanel = buyButton.PriceItem1;
        pricePanel.SetIcon(this.moneyIcon);
        pricePanel.SetPrice(component.Price.ToString());
    }

    private void SetResourcePrice(Component_ResourcePrice component)
    {
        var buyButton = this.view.BuyButton;
        var resources = component.GetPrice();
        var length = resources.Length;
        if (length is < 1 or > 2)
        {
            throw new Exception("Button support only 1 or 2 price items!");
        }

        var resource1 = resources[0];
        var pricePanel1 = buyButton.PriceItem1;
        pricePanel1.SetPrice(resource1.amount.ToString());
        pricePanel1.SetIcon(this.resourceCatalog.FindResource(resource1.type).icon);

        if (length == 1)
        {
            buyButton.SetPriceSize1();
            return;
        }

        var resource2 = resources[1];
        var pricePanel2 = buyButton.PriceItem2;
        pricePanel2.SetPrice(resource2.amount.ToString());
        pricePanel2.SetIcon(this.resourceCatalog.FindResource(resource2.type).icon);

        buyButton.SetPriceSize2();
    }
}