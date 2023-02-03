using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ChestsManager : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    public event Action<Chest> OnChestCountdownStarted;

    public event Action<Chest> OnChestCountdownEnded;

    public event Action<Chest> OnChestActivated;

    public event Action<ChestRewardConfig> OnRewardGenerated;

    public event Action<Chest, ChestRewardConfig> OnRewardReceived;

    public event Action<Chest> OnChestChanged;


    [PropertySpace(8)]
    [ReadOnly]
    [ShowInInspector]
    private readonly Dictionary<ChestType, Chest> _chests = new();

    [SerializeField]
    private ChestCatalog _catalog;

    private IEntity _hero;

    private IComponent_AddExperience _componentAddExp;

    private MoneyStorage _moneyStorage;

    //[PropertySpace(8)]
    //[ReadOnly]
    //[ShowInInspector]
    [SerializeReference]
    private Dictionary<Type, IChestGetReward_Observer> _observers;

    public void AddObserver(Type rewardType, IChestGetReward_Observer observer)
    {
        _observers[rewardType] = observer;
    }

    public void RemoveObserver(Type rewardType) => _observers.Remove(rewardType);

    void IGameInitElement.InitGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
        _hero = context.GetService<HeroService>().GetHero();

        _componentAddExp = _hero.Get<IComponent_AddExperience>();
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        StartAllChests();
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        StopAllChests();
    }



    //[Title("Methods")]
    //[Button]
    //[GUIColor(0, 1, 0)]
    public void ActivateChest(ChestConfig config)
    {
        var chest = config.InstantiateChest(context: this);
        chest.OnCompleted += OnEndChestCoundown;

        _chests[config.ChestType] = chest;

        chest.Start();
        OnChestCountdownStarted?.Invoke(chest);
        OnChestActivated?.Invoke(chest);
        OnChestChanged?.Invoke(chest);
    }


    // Generator for multiple chest options

    //private void GenerateNextChest(string chestId)
    //{
    //    var chestConfig = _catalog.FindChest(chestId);

    //    var nextChest = _factory.CreateChest(chestConfig);
    //    nextChest.OnCompleted += OnEndChestCoundown;

    //    _chests[chestConfig.ChestMetadata.ChestType] = nextChest;

    //    nextChest.Start();
    //    OnChestChanged?.Invoke(nextChest);
    //}

    public Chest InstallChest(ChestConfig config)
    {
        var chest = config.InstantiateChest(context: this);
        chest.OnCompleted += OnEndChestCoundown;

        _chests[config.ChestType] = chest;
        return chest;
    }

    public Chest GetChest(ChestType chestType)
    {
        if (_chests.TryGetValue(chestType, out var chest))
        {
            return chest;
        }

        throw new Exception($"Chest {chestType} is absent!");
    }

    public Chest[] GetActiveChests()
    {
        return _chests.Values.ToArray();
    }

    public bool IsChestExists(ChestType chestType)
    {
        return _chests.ContainsKey(chestType);
    }

    private void StartAllChests()
    {
        foreach (var chest in _chests.Values)
        {
            if (chest.IsActive)
            {
                continue;
            }
            chest.Start();
            OnChestCountdownStarted?.Invoke(chest);
        }
    }

    private void StopAllChests()
    {
        foreach (var chest in _chests.Values)
        {
            if (!chest.IsActive)
            {
                continue;
            }
            chest.OnCompleted -= OnEndChestCoundown;
        }
    }

    private void OnEndChestCoundown(Chest chest)
    {
        chest.OnCompleted -= OnEndChestCoundown;
        StartCoroutine(EndChestInNextFrame(chest));
    }

    private IEnumerator EndChestInNextFrame(Chest chest)
    {
        yield return new WaitForEndOfFrame();
        //_chests.Remove(chest.Config.ChestMetadata.ChestType);
        OnChestCountdownEnded?.Invoke(chest);
        //_factory.DisposeChest(chest);
    }


    public bool CanReceiveReward(Chest chest)
    {
        return chest.IsActive == false &&
               _chests.ContainsValue(chest);
    }
        
    public void ReceiveReward(Chest chest)
    {
        if (!CanReceiveReward(chest))
        {
            throw new Exception($"Can not receive reward from Chest {chest.Id}!");
        }

        var reward = chest.Config.GenerateReward();
        OnRewardGenerated?.Invoke(reward);

        Debug.Log($"<color=red>Reward generated: {reward.RewardMetadata.DisplayName} </color>");
        //Type type = reward.GetType();
        //Debug.Log($"<color=red>Type: {type} </color>");

        //IChestGetReward_Observer chestGetReward_Observer = _observers[type];
        //Debug.Log($"<color=red>Type: {chestGetReward_Observer} </color>");
        //chestGetReward_Observer.OnRewardRecieved(reward);

        OnRewardReceived?.Invoke(chest, reward);
        Debug.Log($"<color=red>Reward generated: {reward.RewardMetadata.DisplayName} </color>");

        ActivateChest(chest.Config);


        //if(reward is ChestRewardConfig_SoftMoney)
        //{
        //    _moneyStorage.EarnMoney(reward.GenerateAmountReward());
        //    Debug.Log("Money Reward recieved.");
        //}
        //else if (reward is ChestRewardConfig_Resource)
        //{
        //    var resource = (ChestRewardConfig_Resource)reward;
        //    var amount = resource.GenerateAmountReward();
        //    var restype = resource.GenerateResourceType();

        //    Debug.Log($"{restype} = {amount} Resources Reward recieved.");
        //}
        //else if (reward is ChestRewardConfig_HardMoney)
        //{
        //    Debug.Log("Crystals Reward recieved.");
        //}
        //else if (reward is ChestRewardConfig_Experience)
        //{
        //    Debug.Log("Experience Reward recieved.");
        //    _componentAddExp.AddExperience(reward.GenerateAmountReward());
        //}



        //GenerateNextChest(chest.Config.ChestMetadata.ChestType);

        //GenerateNextChest(chest.Id);
    }
}