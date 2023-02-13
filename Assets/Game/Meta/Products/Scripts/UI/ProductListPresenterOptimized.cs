using System.Collections.Generic;
using GameSystem;
using UIFrames.Unity;
using UnityEngine;


public sealed class ProductListPresenterOptimized : UnityFrame,
    IGameConstructElement,
    RecyclableViewList.IAdapter
{
    [SerializeField]
    private RecyclableViewList recycleViewList;

    [Space]
    [SerializeField]
    private ProductCatalog productCatalog;

    [SerializeField]
    private ResourceInfoCatalog resourceCatalog;

    [SerializeField]
    private Sprite moneyIcon;

    private readonly Dictionary<RectTransform, ProductPresenter> presenterMap = new();

    private ProductBuyer buyManager;

    private MoneyStorage moneyStorage;

    private ResourceStorage resourceStorage;

    protected override void OnShow(object args)
    {
        this.recycleViewList.Initialize(adapter: this);
    }

    protected override void OnHide()
    {
        this.recycleViewList.Terminate();
    }

    int RecyclableViewList.IAdapter.GetDataCount()
    {
        return this.productCatalog.ProductCount;
    }

    void RecyclableViewList.IAdapter.OnCreateView(RectTransform view, int index)
    {
        var viewComponent = view.GetComponent<ProductView>();
        var presenter = new ProductPresenter(viewComponent);
        presenter.Construct(
            this.buyManager,
            this.moneyStorage,
            this.resourceStorage,
            this.resourceCatalog,
            this.moneyIcon
        );
        this.presenterMap.Add(view, presenter);

        var productConfig = this.productCatalog.GetProduct(index);
        presenter.SetProduct(productConfig.Prototype);
        presenter.Start();
    }

    void RecyclableViewList.IAdapter.OnUpdateView(RectTransform view, int index)
    {
        var presenter = this.presenterMap[view];
        var productConfig = this.productCatalog.GetProduct(index);
        presenter.SetProduct(productConfig.Prototype);
    }

    void RecyclableViewList.IAdapter.OnDestroyView(RectTransform view)
    {
        var presenter = this.presenterMap[view];
        presenter.Stop();
        this.presenterMap.Remove(view);
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        this.buyManager = context.GetService<ProductBuyer>();
        this.moneyStorage = context.GetService<MoneyStorage>();
        this.resourceStorage = context.GetService<ResourceStorage>();
    }
}