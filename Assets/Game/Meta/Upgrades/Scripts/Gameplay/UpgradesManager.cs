using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


public sealed class UpgradesManager : MonoBehaviour,
    IGameAttachElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement,
    IGameDetachElement
{
    public event Action<Upgrade> OnLevelUp;

    public int CurrentMaxLevelOnHero
    {
        get { return _currentMaxLevelOnHero; }
        set { _currentMaxLevelOnHero = value; }
    }

    [FormerlySerializedAs("assets")]
    [SerializeField]
    private UpgradeCatalog _catalog;

    [Space]
    [ReadOnly]
    [ShowInInspector]
    private Dictionary<string, Upgrade> _upgrades;

    private IEntity _hero;

    private MoneyStorage _moneyStorage;

    private IComponent_GetLevel _component_GetLevel;

    private IComponent_OnLevelChanged _component_OnLevelChanged;

    private int _currentMaxLevelOnHero;

    public bool CanLevelUp(Upgrade upgrade)
    {
        if (upgrade.IsMaxUpgradeLevel)
        {
            return false;
        }

        if (upgrade.UpgradeLevel >= _currentMaxLevelOnHero)
        {
            return false;
        }

        var price = upgrade.NextPrice;
        return _moneyStorage.CanSpendMoney(price);
    }

    public void LevelUp(Upgrade upgrade)
    {
        if (!CanLevelUp(upgrade))
        {
            throw new Exception($"Can not level up {upgrade.Id}");
        }

        var price = upgrade.NextPrice;
        _moneyStorage.SpendMoney(price);

        upgrade.LevelUp();
        OnLevelUp?.Invoke(upgrade);
    }

    public Upgrade GetUpgrade(string id)
    {
        return _upgrades[id];
    }

    public Upgrade[] GetAllUpgrades()
    {
        return _upgrades.Values.ToArray<Upgrade>();
    }

    public UpgradesManager()
    {
        _upgrades = new Dictionary<string, Upgrade>();
    }

    private void Awake()
    {
        CreateUpgrades();
    }

    void IGameAttachElement.AttachGame(IGameContext context)
    {
        RegisterUpgrades(context);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
        _hero = context.GetService<HeroService>().GetHero();

        _component_OnLevelChanged = _hero.Get<IComponent_OnLevelChanged>();
        _component_GetLevel = _hero.Get<IComponent_GetLevel>();
        _currentMaxLevelOnHero = _component_GetLevel.Level;
        Debug.Log("Construct Level Hero components");
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _component_OnLevelChanged.OnLevelChanged += UpdateCurrentMaxLevel;
        Debug.Log("Start Onlevelchanged!");
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _component_OnLevelChanged.OnLevelChanged -= UpdateCurrentMaxLevel;
        Debug.Log("Stop Onlevelchanged!");
    }

    private void UpdateCurrentMaxLevel(int currentLevelHero)
    {        
        _currentMaxLevelOnHero = currentLevelHero;
        Debug.Log($"Level {_currentMaxLevelOnHero}");
    }

    void IGameDetachElement.DetachGame(IGameContext context)
    {
        UnregisterUpgrades(context);
    }

    private void CreateUpgrades()
    {
        var configs = this._catalog.GetAllUpgrades();
        for (int i = 0, count = configs.Length; i < count; i++)
        {
            var config = configs[i];
            var upgrade = config.InstantiateUpgrade();
            this._upgrades.Add(config.id, upgrade);
        }
    }

    private void RegisterUpgrades(IGameContext context)
    {
        foreach (var upgrade in this._upgrades.Values)
        {
            if (upgrade is IGameElement gameElement)
            {
                context.RegisterElement(gameElement);
            }
        }
    }

    private void UnregisterUpgrades(IGameContext context)
    {
        foreach (var upgrade in this._upgrades.Values)
        {
            if (upgrade is IGameElement gameElement)
            {
                context.UnregisterElement(gameElement);
            }
        }
    }
}