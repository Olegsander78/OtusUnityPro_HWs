using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using GameSystem;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ScenarioQuestManager : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    public event Action<ScenarioQuest> OnRewardReceived;

    public event Action<ScenarioQuest> OnScenarioQuestChanged;

    [SerializeField]
    private ScenarioQuestFactory _factory;
    
    [SerializeField]
    private ScenarioQuestSelector _selector;

    private IEntity _hero;

    private IComponent_AddExperience _componentAddExp;

    private MoneyStorage _moneyStorage;

    [ReadOnly, ShowInInspector]
    private readonly Dictionary<ScenarioQuestStage, ScenarioQuest> _scenarioQuests = new();

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
        StartScenarioQuest();
    }

    void IGameFinishElement.FinishGame()
    {
        StopScenarioQuest();
    }

    private void StartScenarioQuest()
    {
        foreach (var quest in _scenarioQuests.Values)
        {
            quest.Start();
        }
    }

    private void StopScenarioQuest()
    {
        foreach (var quest in _scenarioQuests.Values)
        {
            quest.Stop();
        }
    }

    public bool CanReceiveReward(ScenarioQuest quest)
    {
        return quest.State == QuestState.COMPLETED &&
               _scenarioQuests.ContainsValue(quest);
    }

    public void ReceiveReward(ScenarioQuest quest)
    {
        if (!CanReceiveReward(quest))
        {
            throw new Exception($"Can not receive reward from ScenarioQuest {quest.Id}!");
        }

        _moneyStorage.EarnMoney(quest.MoneyReward);
        _componentAddExp.AddExperience(quest.ExpReward);
        OnRewardReceived?.Invoke(quest);

        _factory.DisposeQuest(quest);

        var nextStage = quest.StageScenarioQuest + 1;
        if(Enum.IsDefined(typeof(ScenarioQuestStage), nextStage))
        {
            GenerateNextScenarioQuest(nextStage);
        }
        else
        {
            throw new Exception($"Scenario Quest {nextStage} is absent. Scenario Quests finished!");
        }        
    }

    public ScenarioQuest GetScenarioQuest(ScenarioQuestStage stage)
    {
        if (_scenarioQuests.TryGetValue(stage, out var quest))
        {
            return quest;
        }

        throw new Exception($"Scenario Quest {stage} is absent!");
    }

    public ScenarioQuest[] GetScenarioQuests()
    {
        return _scenarioQuests.Values.ToArray();
    }

    public bool IsScenarioQuestExists(ScenarioQuestStage stage)
    {
        return _scenarioQuests.ContainsKey(stage);
    }

    public ScenarioQuest InstallScenarioQuest(ScenarioQuestConfig scenarioQuestInfo)
    {
        var scenarioQuest = _factory.CreateQuest(scenarioQuestInfo);
        _scenarioQuests[scenarioQuestInfo.ScenarioQuestStage] = scenarioQuest;
        return scenarioQuest;
    }    

    private void GenerateNextScenarioQuest(ScenarioQuestStage stage)
    {
        var scenarioQuestConfig = _selector.SelectNextScenarioQuest(stage);
        var scenarioQuest = _factory.CreateQuest(scenarioQuestConfig);
        _scenarioQuests[stage] = scenarioQuest;

        scenarioQuest.Start();
        OnScenarioQuestChanged?.Invoke(scenarioQuest); // Event for GameStageSystem
    }


}