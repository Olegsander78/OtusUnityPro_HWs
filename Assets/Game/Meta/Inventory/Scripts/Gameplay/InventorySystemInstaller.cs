using GameSystem;
using Sirenix.OdinInspector;
using static GameSystem.GameComponentType;


public sealed class InventorySystemInstaller : MonoGameInstaller
{
    private readonly StackableInventory inventory = new();

    [GameComponent(SERVICE)]
    [ReadOnly, ShowInInspector]
    private InventoryService service = new();

    [GameComponent(SERVICE)]
    [ShowInInspector]
    private InventoryItemConsumer itemConsumer = new();

    [GameComponent(SERVICE)]
    [ShowInInspector]
    private InventoryItemCrafter itemCrafter = new();

    [GameComponent(SERVICE)]
    [ShowInInspector]
    private InventoryItemEquipper itemEquipper = new();

    //[GameComponent(ELEMENT)]
    //[ReadOnly, ShowInInspector]
    //private InventoryItemsEffectController effectsObserver = new();

    public override void ConstructGame(IGameContext context)
    {
        this.service.Setup(this.inventory);
        this.itemConsumer.SetInventory(this.inventory);
        this.itemCrafter.SetInventory(this.inventory);
        this.itemEquipper.SetInventory(this.inventory);

        //this.InstallEffectObserver(context);
        this.InstallConsumeHealingKit(context);
        this.InstallProductBuyKit(context);
        this.InstallEquipmentKit(context);
    }

    //private void InstallEffectObserver(IGameContext context)
    //{
    //    var heroService = context.GetService<HeroService>();
    //    this.effectsObserver.Construct(heroService);
    //    this.effectsObserver.SetInventory(this.inventory);
    //}

    private void InstallConsumeHealingKit(IGameContext context)
    {
        var heroService = context.GetService<HeroService>();
        this.itemConsumer.AddHandler(new InventoryItemConsumeHandler_HealingPoints(heroService));
    }

    private void InstallProductBuyKit(IGameContext context)
    {
        var buySystem = context.GetService<ProductBuyer>();
        buySystem.AddCompletor(new ProductBuyCompletor_AddInventoryItem(this.inventory));
    }

    private void InstallEquipmentKit(IGameContext context)
    {
        var heroService = context.GetService<HeroService>();
        this.itemEquipper.Construct(heroService);
        this.itemEquipper.AddHandler(new InventoryItemEquipHandler_ItemEffect(heroService));

    }

    [Title("Debug")]
    [Button]
    private void AddItems(InventoryItemConfig itemInfo, int count)
    {
        this.inventory.AddItemsByPrototype(itemInfo.Prototype, count);
    }

    [Button]
    private void RemoveItem(InventoryItemConfig itemInfo, int count)
    {
        this.inventory.RemoveItems(itemInfo.ItemName, count);
    }
}