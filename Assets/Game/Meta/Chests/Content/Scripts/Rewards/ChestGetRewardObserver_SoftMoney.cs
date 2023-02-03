using GameElements;
using Services;
using System;
using UnityEngine;

//[Serializable]
public class ChestGetRewardObserver_SoftMoney : ChestRewardObserver
    //IChestGetReward_Observer
{
    
    private MoneyStorage _moneyStorage;

    public override void InitGame(IGameContext context)
    {
        base.InitGame(context);
        _moneyStorage = context.GetService<MoneyStorage>();
    }

    //[Inject]
    //private ChestsManager _chestsManager;

    //public ChestGetRewardObserver_SoftMoney(MoneyStorage moneyStorage, ChestsManager chestsManager)
    //{
    //    _moneyStorage = moneyStorage;
    //    _chestsManager = chestsManager;
    //}

    //[Inject]
    //public void Construct(MoneyStorage moneyStorage, ChestsManager chestsManager)
    //{
    //    _moneyStorage = moneyStorage;
    //    Debug.Log("<color=red>MoneyStorage injected in getreward_softmoney</color>");
    //    _chestsManager = chestsManager;
    //    _chestsManager.AddObserver(typeof(ChestGetRewardObserver_SoftMoney), this);
    //}

    //void IGameReadyElement.ReadyGame(IGameContext context)
    //{
    //    _moneyStorage = context.GetService<MoneyStorage>();
    //    Debug.Log("<color=red>MoneyStorage injected in getreward_softmoney</color>");
    //    _chestsManager = context.GetService<ChestsManager>();
    //    _chestsManager.AddObserver(typeof(ChestGetRewardObserver_SoftMoney), this);
    //}

    //public void OnRewardRecieved(ChestRewardConfig chestRewardConfig)
    //{
    //    if (chestRewardConfig is ChestRewardConfig_SoftMoney)
    //    {
    //        _moneyStorage.EarnMoney(chestRewardConfig.GenerateAmountReward());
    //        Debug.Log("Money Reward recieved.");
    //    }
    //}

    protected override void OnRewardRecieved(ChestRewardConfig chestRewardConfig)
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

 
