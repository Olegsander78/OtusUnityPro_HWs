using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using static GameSystem.GameComponentType;


public sealed class BoostersSystemInstaller : MonoGameInstaller
{
    [GameComponent(SERVICE | ELEMENT)]
    [ShowInInspector]
    private BoostersManager manager = new();

    [GameComponent]
    [ReadOnly, ShowInInspector]
    private readonly BoosterFactory factory = new();

    //[GameComponent(ELEMENT)]
    //[Space, ReadOnly, ShowInInspector]
    //private readonly BoosterAnalyticsTracker analyticsTracker = new();

    public override void ConstructGame(IGameContext context)
    {
        base.ConstructGame(context);
        this.ConstructControllers(context);
    }

    private void ConstructControllers(IGameContext context)
    {
        var consumeManager = context.GetService<InventoryItemConsumer>();
        consumeManager.AddHandler(new InventoryItemConsumeHandler_ActivateBooster(this.manager));

        var productManager = context.GetService<ProductBuyer>();
        productManager.AddCompletor(new ProductBuyCompletor_ActivateBooster(this.manager));

        var timeShiftEmitter = context.GetService<TimeShiftEmitter>();
        timeShiftEmitter.AddListener(new TimeShiftObserver_SyncBoosters(this.manager));
    }
}