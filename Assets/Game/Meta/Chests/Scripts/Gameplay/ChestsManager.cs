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

    public event Action<Chest> OnRewardReceived;

    public event Action<Chest> OnChestChanged;

    [SerializeField]
    private ChestFactory _factory;

    //[PropertySpace(8)]
    //[ReadOnly]
    //[ShowInInspector]
    //private readonly List<Chest> _chests = new();

    //private readonly List<Chest> _buffer = new();

    [PropertySpace(8)]
    [ReadOnly]
    [ShowInInspector]
    private readonly Dictionary<ChestType, Chest> _chests = new();

    [SerializeField]
    private ChestCatalog _catalog;

    private IEntity _hero;

    private IComponent_AddExperience _componentAddExp;

    private MoneyStorage _moneyStorage;


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
        var chest = _factory.CreateChest(config);
        chest.OnCompleted += OnEndChestCoundown;

        _chests[config.ChestMetadata.ChestType] = chest;

        chest.Start();
        OnChestCountdownStarted?.Invoke(chest);
        OnChestActivated?.Invoke(chest);
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
        var chest = _factory.CreateChest(config);
        chest.OnCompleted += OnEndChestCoundown;

        _chests[config.ChestMetadata.ChestType] = chest;
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
            //chest.Stop();
        }
    }

    private void OnEndChestCoundown(Chest chest)
    {
        chest.OnCompleted -= OnEndChestCoundown;
        StartCoroutine(EndChestInNextFrame(chest));
        ReceiveReward(chest);
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

    [Button]
    public void ReceiveReward(Chest chest)
    {
        if (!CanReceiveReward(chest))
        {
            throw new Exception($"Can not receive reward from Chest {chest.Id}!");
        }

        if(chest.Reward is ChestRewardConfig_SoftMoney)
        {
            _moneyStorage.EarnMoney(chest.Reward.Amount);
            Debug.Log("Money Reward recieved.");
        }
        else if (chest.Reward is ChestRewardConfig_Resource)
        {
            Debug.Log("Resources Reward recieved.");
        }
        else if (chest.Reward is ChestRewardConfig_HardMoney)
        {
            Debug.Log("Crystals Reward recieved.");
        }
        else if (chest.Reward is ChestRewardConfig_Experience)
        {
            Debug.Log("Experience Reward recieved.");
            _componentAddExp.AddExperience(chest.Reward.Amount);
        }

        OnRewardReceived?.Invoke(chest);

        _factory.DisposeChest(chest);
        //GenerateNextChest(chest.Config.ChestMetadata.ChestType);
        ActivateChest(chest.Config);
        //GenerateNextChest(chest.Id);
    }
}