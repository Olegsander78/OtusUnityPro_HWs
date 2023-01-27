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

    [Title("Methods")]
    [Button]
    [GUIColor(0, 1, 0)]
    public void ActivateChest(ChestConfig config)
    {
        var chest = _factory.CreateChest(config);
        chest.OnCompleted += OnEndChestCoundown;

        _chests[config.ChestMetadata.ChestType] = chest;

        chest.Start();
        OnChestCountdownStarted?.Invoke(chest);
        OnChestActivated?.Invoke(chest);
    }

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

        //_buffer.Clear();
        //_buffer.AddRange(_chests);

        //for (int i = 0, count = _buffer.Count; i < count; i++)
        //{
        //    var chest = _buffer[i];
        //    if (chest.IsActive)
        //    {
        //        continue;
        //    }

        //    chest.Start();
        //    OnChestCountdownStarted?.Invoke(chest);
        //}
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
            chest.Stop();
        }

        //_buffer.Clear();
        //_buffer.AddRange(_chests);

        //for (int i = 0, count = _buffer.Count; i < count; i++)
        //{
        //    var chest = _buffer[i];
        //    if (!chest.IsActive)
        //    {
        //        continue;
        //    }

        //    chest.OnCompleted -= OnEndChestCoundown;
        //    //chest.Stop();
        //}
    }

    private void OnEndChestCoundown(Chest chest)
    {
        chest.OnCompleted -= OnEndChestCoundown;
        StartCoroutine(EndChestInNextFrame(chest));
    }

    private IEnumerator EndChestInNextFrame(Chest chest)
    {
        yield return new WaitForEndOfFrame();
        _chests.Remove(chest.Config.ChestMetadata.ChestType);
        OnChestCountdownEnded?.Invoke(chest);
        _factory.DisposeChest(chest);
    }
}