using System;
using GameElements;
using UnityEngine;


public sealed class ScenarioQuestListPresenter : MonoBehaviour, IGameInitElement
{
    [SerializeField]
    private ScenarioQuestItem[] _scenarioQuestItems;

    private ScenarioQuestManager _scenarioQuestsManager;

    public void Show()
    {
        _scenarioQuestsManager.OnScenarioQuestChanged += OnScenarioQuestChanged;

        var scenarioQuests = _scenarioQuestsManager.GetScenarioQuests();
        for (int i = 0, count = scenarioQuests.Length; i < count; i++)
        {
            var scenarioQuest = scenarioQuests[i];
            var presenter = GetPresenter(scenarioQuest.StageScenarioQuest);
            presenter.Start(scenarioQuest);
        }
    }

    public void Hide()
    {
        _scenarioQuestsManager.OnScenarioQuestChanged -= OnScenarioQuestChanged;

        for (int i = 0, count = _scenarioQuestItems.Length; i < count; i++)
        {
            var presenter = _scenarioQuestItems[i].Presenter;
            presenter.Stop();
        }
    }

    private void OnScenarioQuestChanged(ScenarioQuest scenarioQuest)
    {
        var presenter = GetPresenter(scenarioQuest.StageScenarioQuest);
        if (presenter.IsShown)
        {
            presenter.Stop();
        }

        presenter.Start(scenarioQuest);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _scenarioQuestsManager = context.GetService<ScenarioQuestManager>();
        //var moneyPanelAnimator = context.GetService<MoneyPanelAnimator_AddMoney>();

        for (int i = 0, count = _scenarioQuestItems.Length; i < count; i++)
        {
            var questItem = _scenarioQuestItems[i];
            questItem.Presenter.Construct(_scenarioQuestsManager);
        }
    }

    private ScenarioQuestPresenter GetPresenter(ScenarioQuestStage stage)
    {
        for (int i = 0, count = _scenarioQuestItems.Length; i < count; i++)
        {
            var questItem = _scenarioQuestItems[i];
            if (questItem.Stage == stage)
            {
                return questItem.Presenter;
            }
        }

        throw new Exception($"Scenario Quest {stage} is not found"!);
    }

    [Serializable]
    private sealed class ScenarioQuestItem
    {
        [SerializeField]
        public ScenarioQuestStage Stage;

        [SerializeField]
        public ScenarioQuestPresenter Presenter;
    }
}