using System.Collections.Generic;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ChestManagerTest : MonoBehaviour, IGameInitElement
{
    [ShowInInspector, ReadOnly]
    private Chest _chest;

    private ResourceStorage _resourceStorage;

    private MoneyStorage _moneyStorage;

    private Dictionary<string, Chest> _chests;

    [Button]
    private void ActivateChest(ChestConfig config)
    {
        //if (_moneyStorage.Money < config.ChestMetadata.PriceOpen)
        //{
        //    return;
        //}

        //_moneyStorage.SpendMoney(config.);

        _chest = config.InstantiateChest(context: this);
        //GameInjector.Inject(this._gameContext, this._chest);

        _chest.Start();
    }

    void IGameInitElement.InitGame(IGameContext context)
    {        
        _moneyStorage = context.GetService<MoneyStorage>();
        _resourceStorage = context.GetService<ResourceStorage>();
    }
}