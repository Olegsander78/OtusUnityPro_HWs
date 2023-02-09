using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using GameSystem;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class DailyQuestManager : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    public event Action<DailyQuest> OnRewardReceived;

    public event Action<DailyQuest> OnDailyQuestChanged;

    [SerializeField]
    private DailyQuestFactory _factory;
    
    [SerializeField]
    private DailyQuestSelector _selector;

    private IEntity _hero;

    private IComponent_AddExperience _componentAddExp;

    private MoneyStorage _moneyStorage;

    [ReadOnly, ShowInInspector]
    private readonly Dictionary<DailyQuestDifficulty, DailyQuest> _dailyQuests = new();


    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
        _hero = context.GetService<HeroService>().GetHero();
    }

    void IGameInitElement.InitGame()
    {
        _componentAddExp = _hero.Get<IComponent_AddExperience>();
    }

    void IGameStartElement.StartGame()
    {
        StartDailyQuests();
    }

    void IGameFinishElement.FinishGame()
    {
        StopDailyQuests();
    }

    private void StartDailyQuests()
    {
        foreach (var mission in _dailyQuests.Values)
        {
            mission.Start();
        }
    }

    private void StopDailyQuests()
    {
        foreach (var mission in _dailyQuests.Values)
        {
            mission.Stop();
        }
    }

    public bool CanReceiveReward(DailyQuest mission)
    {
        return mission.State == QuestState.COMPLETED &&
               _dailyQuests.ContainsValue(mission);
    }

    public void ReceiveReward(DailyQuest mission)
    {
        if (!CanReceiveReward(mission))
        {
            throw new Exception($"Can not receive reward from _chest {mission.Id}!");
        }

        _moneyStorage.EarnMoney(mission.MoneyReward);
        _componentAddExp.AddExperience(mission.ExpReward);
        OnRewardReceived?.Invoke(mission);

        _factory.DisposeQuest(mission);
        GenerateNextDailyQuest(mission.Difficulty, mission.Id);
    }

    public DailyQuest GetDailyQuest(DailyQuestDifficulty difficulty)
    {
        if (_dailyQuests.TryGetValue(difficulty, out var mission))
        {
            return mission;
        }

        throw new Exception($"DailyQuest {difficulty} is absent!");
    }

    public DailyQuest[] GetDailyQuests()
    {
        return _dailyQuests.Values.ToArray();
    }

    public bool IsDailyQuestExists(DailyQuestDifficulty difficulty)
    {
        return _dailyQuests.ContainsKey(difficulty);
    }

    public DailyQuest InstallDailyQuest(DailyQuestConfig dailyQuestInfo)
    {
        var dailyQuest = _factory.CreateQuest(dailyQuestInfo);
        _dailyQuests[dailyQuestInfo.Difficulty] = dailyQuest;
        return dailyQuest;
    }    

    private void GenerateNextDailyQuest(DailyQuestDifficulty difficulty, string prevDailyQuestId)
    {
        var dailyQuestConfig = _selector.SelectNextDailyQuest(difficulty, prevDailyQuestId);
        var dailyQuest = _factory.CreateQuest(dailyQuestConfig);
        _dailyQuests[difficulty] = dailyQuest;

        dailyQuest.Start();
        OnDailyQuestChanged?.Invoke(dailyQuest);
    }
}