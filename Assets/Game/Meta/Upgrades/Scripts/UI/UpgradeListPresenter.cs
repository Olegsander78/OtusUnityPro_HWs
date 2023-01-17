using System.Collections.Generic;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class UpgradeListPresenter : MonoBehaviour, 
    IGameInitElement
{
    [SerializeField]
    private UpgradeView _viewPrefab;

    [SerializeField]
    private Transform _viewContainer;

    private UpgradesManager _upgradesManager;

    private MoneyStorage _moneyStorage;

    private readonly List<UpgradeView> _activeViews = new();

    private readonly List<UpgradePresenter> _activePresenters = new();

    [Button]
    public void Show()
    {
        Upgrade[] upgrades = _upgradesManager.GetAllUpgrades();
        foreach (var upgrade in upgrades)
        {
            UpgradeView view = Instantiate(_viewPrefab, _viewContainer); 
            _activeViews.Add(view);

            UpgradePresenter presenter = new UpgradePresenter(upgrade, view);
            presenter.Construct(_upgradesManager, this._moneyStorage); //DI
            _activePresenters.Add(presenter);
        }

        foreach (var presenter in _activePresenters)
        {
            presenter.Start();
        }
    }

    [Button]
    public void Hide()
    {
        foreach (var presenter in _activePresenters)
        {
            presenter.Stop();
        }

        _activePresenters.Clear();

        foreach (var view in _activeViews)
        {
            Destroy(view.gameObject); 
        }

        _activeViews.Clear();
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _upgradesManager = context.GetService<UpgradesManager>();
        _moneyStorage = context.GetService<MoneyStorage>();
    }
}