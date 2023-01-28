using System;
using GameElements;
using UnityEngine;


public sealed class DailyQuestListPresenter : MonoBehaviour, IGameInitElement
{
    [SerializeField]
    private DailyQuestItem[] dailyQuestItems;

    private DailyQuestManager dailyQuestsManager;

    public void Show()
    {
        this.dailyQuestsManager.OnDailyQuestChanged += this.OnDailyQuestChanged;

        var dailyQuests = this.dailyQuestsManager.GetDailyQuests();
        for (int i = 0, count = dailyQuests.Length; i < count; i++)
        {
            var dailyQuest = dailyQuests[i];
            var presenter = this.GetPresenter(dailyQuest.Difficulty);
            presenter.Start(dailyQuest);
        }
    }

    public void Hide()
    {
        this.dailyQuestsManager.OnDailyQuestChanged -= this.OnDailyQuestChanged;

        for (int i = 0, count = this.dailyQuestItems.Length; i < count; i++)
        {
            var presenter = this.dailyQuestItems[i].presenter;
            presenter.Stop();
        }
    }

    private void OnDailyQuestChanged(DailyQuest dailyQuest)
    {
        var presenter = this.GetPresenter(dailyQuest.Difficulty);
        if (presenter.IsShown)
        {
            presenter.Stop();
        }

        presenter.Start(dailyQuest);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.dailyQuestsManager = context.GetService<DailyQuestManager>();
        //var moneyPanelAnimator = context.GetService<MoneyPanelAnimator_AddMoney>();

        for (int i = 0, count = this.dailyQuestItems.Length; i < count; i++)
        {
            var missionItem = this.dailyQuestItems[i];
            missionItem.presenter.Construct(this.dailyQuestsManager);
        }
    }

    private DailyQuestPresenter GetPresenter(DailyQuestDifficulty difficulty)
    {
        for (int i = 0, count = this.dailyQuestItems.Length; i < count; i++)
        {
            var missionItem = this.dailyQuestItems[i];
            if (missionItem.difficulty == difficulty)
            {
                return missionItem.presenter;
            }
        }

        throw new Exception($"DailyQuest with TypeChest {difficulty} is not found"!);
    }

    [Serializable]
    private sealed class DailyQuestItem
    {
        [SerializeField]
        public DailyQuestDifficulty difficulty;

        [SerializeField]
        public DailyQuestPresenter presenter;
    }
}