using System;
using GameSystem;
using UnityEngine;


public sealed class ChestListPresenter : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement
{
    [SerializeField]
    private ChestItem[] _chestItems;

    private ChestsManager _chestsManager;

    public void Show()
    {
        _chestsManager.OnChestChanged += OnChestChanged;

        var chests = _chestsManager.GetActiveChests();
        for (int i = 0, count = chests.Length; i < count; i++)
        {
            var chest = chests[i];
            var presenter = GetPresenter(chest.Config.ChestType);
            presenter.Start(chest);
        }
    }

    public void Hide()
    {
        _chestsManager.OnChestChanged -= OnChestChanged;

        for (int i = 0, count = _chestItems.Length; i < count; i++)
        {
            var presenter = _chestItems[i].Presenter;
            presenter.Stop();
        }
    }

    private void OnChestChanged(Chest chest)
    {
        var presenter = GetPresenter(chest.Config.ChestType);
        if (presenter.IsShown)
        {
            presenter.Stop();
        }

        presenter.Start(chest);
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _chestsManager = context.GetService<ChestsManager>();
    }

    void IGameInitElement.InitGame()
    {
        
        //var moneyPanelAnimator = context.GetService<MoneyPanelAnimator_AddMoney>();

        for (int i = 0, count = _chestItems.Length; i < count; i++)
        {
            var chestItem = _chestItems[i];
            chestItem.Presenter.Construct(_chestsManager);
        }
    }

    private ChestPresenter GetPresenter(ChestType typeChest)
    {
        for (int i = 0, count = _chestItems.Length; i < count; i++)
        {
            var chestItem = _chestItems[i];
            if (chestItem.TypeChest == typeChest)
            {
                return chestItem.Presenter;
            }
        }

        throw new Exception($"Chest with TypeChest {typeChest} is not found"!);
    }



    [Serializable]
    private sealed class ChestItem
    {
        [SerializeField]
        public ChestType TypeChest;

        [SerializeField]
        public ChestPresenter Presenter;
    }
}