using System.Collections.Generic;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class UpgradeListPresenter : MonoBehaviour, 
    IGameInitElement
{
    [SerializeField]
    private UpgradeView viewPrefab;

    [SerializeField]
    private Transform viewContainer;

    private UpgradesManager upgradesManager;

    private MoneyStorage moneyStorage;

    private readonly List<UpgradeView> activeViews = new();

    private readonly List<UpgradePresenter> activePresenters = new();

    [Button]
    public void Show()
    {
        Upgrade[] upgrades = this.upgradesManager.GetAllUpgrades();
        foreach (var upgrade in upgrades)
        {
            UpgradeView view = Instantiate(this.viewPrefab, this.viewContainer); //
            this.activeViews.Add(view);

            UpgradePresenter presenter = new UpgradePresenter(upgrade, view);
            presenter.Construct(this.upgradesManager, this.moneyStorage); //DI
            this.activePresenters.Add(presenter);
        }

        foreach (var presenter in this.activePresenters)
        {
            presenter.Start();
        }
    }

    [Button]
    public void Hide()
    {
        foreach (var presenter in this.activePresenters)
        {
            presenter.Stop();
        }

        this.activePresenters.Clear();

        foreach (var view in this.activeViews)
        {
            Destroy(view.gameObject); //
        }

        this.activeViews.Clear();
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.upgradesManager = context.GetService<UpgradesManager>();
        this.moneyStorage = context.GetService<MoneyStorage>();
    }
}