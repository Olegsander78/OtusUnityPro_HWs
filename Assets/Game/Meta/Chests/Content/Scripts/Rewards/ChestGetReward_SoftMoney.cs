using GameElements;
using Services;
using System;
using UnityEngine;


public class ChestGetReward_SoftMoney : IChestGetReward_Observer,
    IGameReadyElement
{
    [Inject]
    private MoneyStorage _moneyStorage;

    [Inject]
    private ChestsManager _chestsManager;

    public ChestGetReward_SoftMoney(MoneyStorage moneyStorage, ChestsManager chestsManager)
    {
        _moneyStorage = moneyStorage;
        _chestsManager = chestsManager;
    }

    [Inject]
    public void Construct(MoneyStorage moneyStorage, ChestsManager chestsManager)
    {
        _moneyStorage = moneyStorage;
        Debug.Log("<color=red>MoneyStorage injected in getreward_softmoney</color>");
        _chestsManager = chestsManager;
        _chestsManager.AddObserver(typeof(ChestGetReward_SoftMoney), this);
    }

    void IGameReadyElement.ReadyGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
        Debug.Log("<color=red>MoneyStorage injected in getreward_softmoney</color>");
        _chestsManager = context.GetService<ChestsManager>();
        _chestsManager.AddObserver(typeof(ChestGetReward_SoftMoney), this);
    }

    public void OnRewardRecieved(ChestRewardConfig chestRewardConfig)
    {
        if (chestRewardConfig is ChestRewardConfig_SoftMoney)
        {
            _moneyStorage.EarnMoney(chestRewardConfig.GenerateAmountReward());
            Debug.Log("Money Reward recieved.");
        }
    }



    //void IGameConstructElement.ConstructGame(IGameContext context)
    //{
    //    _moneyStorage = context.GetService<MoneyStorage>();
    //    Debug.Log("<color=red>MoneyStorage injected in getreward_softMoney</color>");
    //}

    //void IGameInitElement.InitGame(IGameContext context)
    //{
    //    _moneyStorage = context.GetService<MoneyStorage>();
    //} 
}

 
