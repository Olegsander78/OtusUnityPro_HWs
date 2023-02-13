using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using static GameSystem.GameComponentType;


public sealed class ProductSystemInstaller : MonoGameInstaller
{
    [GameComponent(SERVICE)]
    [ShowInInspector]
    private ProductBuyer productBuyer = new();

    //[GameComponent(ELEMENT)]
    //[Space, ReadOnly, ShowInInspector]
    //private BuyProductAnalyticsTrackerV1 analyticsTracker = new();

    public override void ConstructGame(IGameContext context)
    {
        base.ConstructGame(context);
        this.InstallMoneyKit(context);
        this.InstallResourcesKit(context);
    }

    private void InstallMoneyKit(IGameContext context)
    {
        var moneyBank = context.GetService<MoneyStorage>();
        this.productBuyer.AddCondition(new ProductBuyCondition_CanSpendMoney(moneyBank));
        this.productBuyer.AddProcessor(new ProductBuyProcessor_SpendMoney(moneyBank));
    }

    private void InstallResourcesKit(IGameContext context)
    {
        var resourceStorage = context.GetService<ResourceStorage>();
        this.productBuyer.AddCondition(new ProductBuyCondition_CanSpendResources(resourceStorage));
        this.productBuyer.AddProcessor(new ProductBuyProcessor_SpendResources(resourceStorage));
    }
}