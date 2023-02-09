using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ChestsManager :
    IGameStartElement,
    IGameFinishElement
{
    public event Action<Chest> OnChestCountdownStarted;

    public event Action<Chest> OnChestCountdownEnded;

    public event Action<Chest> OnChestActivated;

    public event Action<Chest, ChestRewardConfig> OnRewardReceived;

    public event Action<Chest> OnChestChanged;


    [PropertySpace(8)]
    [ReadOnly]
    [ShowInInspector]
    private readonly Dictionary<ChestType, Chest> _chests = new();

    [PropertySpace(8)]
    [ReadOnly]
    [ShowInInspector]
    private readonly List<IChestRewardObserver> _rewardObservers = new();

    [PropertySpace(8)]
    [ShowInInspector]
    private MonoBehaviour _monoContext;

    public void AddObserver(IChestRewardObserver observer)
    {
        _rewardObservers.Add(observer);
    }

    public void RemoveObserver(IChestRewardObserver observer)
    {
        _rewardObservers.Remove(observer);
    }    

    public void Construct(MonoBehaviour monoContext)
    {
        _monoContext = monoContext;
    }

    void IGameStartElement.StartGame()
    {
        StartAllChests();
    }

    void IGameFinishElement.FinishGame()
    {
        StopAllChests();
    }

    public void ActivateChest(ChestConfig config)
    {
        var chest = config.InstantiateChest(_monoContext);
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
        var chest = config.InstantiateChest(_monoContext);
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
        _monoContext.StartCoroutine(EndChestInNextFrame(chest));
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

        foreach (var observer in _rewardObservers)
        {
            observer.OnRewardReceived(reward);
        }

        Debug.Log($"<color=red>Reward generated: {reward.RewardMetadata.DisplayName} </color>");
        //Type type = reward.GetType();
        //Debug.Log($"<color=red>Type: {type} </color>");

        //IChestGetReward_Observer chestGetReward_Observer = _observers[type];
        //Debug.Log($"<color=red>Type: {chestGetReward_Observer} </color>");
        //chestGetReward_Observer.OnRewardRecieved(reward);

        OnRewardReceived?.Invoke(chest, reward);
        Debug.Log($"<color=red>Reward generated: {reward.RewardMetadata.DisplayName} </color>");

        ActivateChest(chest.Config);       
    }
}