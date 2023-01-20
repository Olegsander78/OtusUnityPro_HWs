using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using GameElements;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class MissionsManager : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    public event Action<Mission> OnRewardReceived;

    public event Action<Mission> OnMissionChanged;

    [SerializeField]
    private MissionFactory _factory;
    
    [SerializeField]
    private MissionSelector _selector;

    private IEntity _hero;

    private IComponent_AddExperience _componentAddExp;

    private MoneyStorage _moneyStorage;

    [ReadOnly, ShowInInspector]
    private readonly Dictionary<MissionDifficulty, Mission> _missions = new();

    //[Inject]
    //public void Construct(MissionFactory factory, MissionSelector selector, MoneyStorage moneyStorage)
    //{
    //    _factory = factory;
    //    _selector = selector;
    //    _moneyStorage = moneyStorage;
    //    Debug.Log($"{_factory} ,{_selector}, {_moneyStorage} Construct success!");
    //}

    //public MissionsManager()
    //{
    //    _missions = new Dictionary<MissionDifficulty, Mission>();
    //}

    void IGameInitElement.InitGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
        _hero = context.GetService<HeroService>().GetHero();

        _componentAddExp = _hero.Get<IComponent_AddExperience>();
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        StartMissions();
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        StopMissions();
    }

    private void StartMissions()
    {
        foreach (var mission in _missions.Values)
        {
            mission.Start();
        }
    }

    private void StopMissions()
    {
        foreach (var mission in _missions.Values)
        {
            mission.Stop();
        }
    }

    public bool CanReceiveReward(Mission mission)
    {
        return mission.State == MissionState.COMPLETED &&
               _missions.ContainsValue(mission);
    }

    public void ReceiveReward(Mission mission)
    {
        if (!CanReceiveReward(mission))
        {
            throw new Exception($"Can not receive reward from mission {mission.Id}!");
        }

        _moneyStorage.EarnMoney(mission.MoneyReward);
        _componentAddExp.AddExperience(mission.ExpReward);
        OnRewardReceived?.Invoke(mission);

        _factory.DisposeMission(mission);
        GenerateNextMission(mission.Difficulty, mission.Id);
    }

    public Mission GetMission(MissionDifficulty difficulty)
    {
        if (_missions.TryGetValue(difficulty, out var mission))
        {
            return mission;
        }

        throw new Exception($"Mission {difficulty} is absent!");
    }

    public Mission[] GetMissions()
    {
        return _missions.Values.ToArray();
    }

    public bool IsMissionExists(MissionDifficulty difficulty)
    {
        return _missions.ContainsKey(difficulty);
    }

    public Mission InstallMission(MissionConfig missionInfo)
    {
        var mission = _factory.CreateMission(missionInfo);
        _missions[missionInfo.Difficulty] = mission;
        return mission;
    }    

    private void GenerateNextMission(MissionDifficulty difficulty, string prevMissionId)
    {
        var missionConfig = _selector.SelectNextMission(difficulty, prevMissionId);
        var mission = _factory.CreateMission(missionConfig);
        _missions[difficulty] = mission;

        mission.Start();
        OnMissionChanged?.Invoke(mission);
    }
}