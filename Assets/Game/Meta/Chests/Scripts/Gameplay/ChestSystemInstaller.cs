using GameElements;
using Services;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public sealed class ChestSystemInstaller: MonoBehaviour,
    IGameServiceGroup,
    IGameElementGroup,
    IGameInitElement,
    IGameConstructElement
{
    [SerializeField]
    private ChestConfig _woodenChest;

    [SerializeField]
    private ChestConfig _steelChest;

    [SerializeField]
    private ChestConfig _goldenChest;

    [Space()]
    [ShowInInspector,ReadOnly]
    private ChestsManager _chestsManager = new();

    public IEnumerable<object> GetServices()
    {
        Debug.Log("GET SERVICES");
        yield return _chestsManager;
    }

    public IEnumerable<IGameElement> GetElements()
    {
        Debug.Log("GET ELEMENTS");
        yield return _chestsManager;
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        Debug.Log("CONSTRUCT");
        _chestsManager.Construct(monoContext: this);
        _chestsManager.AddObserver(new ChestGetRewardObserver_SoftMoney(context.GetService<MoneyStorage>()));
        _chestsManager.AddObserver(new ChestGetRewardObserver_Experience(context.GetService<HeroService>()));
        _chestsManager.AddObserver(new ChestGetRewardObserver_Resource(context.GetService<ResourceStorage>()));
        _chestsManager.AddObserver(new ChestGetRewardObserver_HardMoney());
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        Debug.Log("INIT");
        if (!_chestsManager.IsChestExists(ChestType.WOODEN_CHEST))
        {
            _chestsManager.InstallChest(_woodenChest);
        }

        if (!_chestsManager.IsChestExists(ChestType.STEEL_CHEST))
        {
            _chestsManager.InstallChest(_steelChest);
        }

        if (!_chestsManager.IsChestExists(ChestType.GOLD_CHEST))
        {
            _chestsManager.InstallChest(_goldenChest);
        }
    }
}
