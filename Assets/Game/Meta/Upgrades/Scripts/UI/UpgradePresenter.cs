using UnityEngine;


public sealed class UpgradePresenter
{
    private readonly Upgrade _upgrade;

    private readonly UpgradeView _view;

    private UpgradesManager _upgradesManager;

    private MoneyStorage _moneyStorage;

    public UpgradePresenter(Upgrade upgrade, UpgradeView view)
    {
        _upgrade = upgrade;
        _view = view;
    }

    public void Construct(UpgradesManager upgradesManager, MoneyStorage moneyStorage)
    {
        _upgradesManager = upgradesManager;
        _moneyStorage = moneyStorage;
    }

    public void Start()
    {
        _view.UpgradeButton.AddListener(OnButtonClicked);
        _upgrade.OnUpgradeUp += OnLevelUp;
        _moneyStorage.OnMoneyChanged += OnMoneyChanged;
        //LanguageManager.OnLanguageChanged += this.OnLanguageChanged;

        UpdateTitle();
        UpdateLevel();
        UpdateIcon();
        UpdateStats();
        UpdateButtonPrice();
        UpdateButtonState();
    }

    public void Stop()
    {
        _view.UpgradeButton.RemoveListener(OnButtonClicked);
        _upgrade.OnUpgradeUp -= OnLevelUp;
        _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        //LanguageManager.OnLanguageChanged -= this.OnLanguageChanged;
    }

    //Model
    private void OnLevelUp(int newLevel)
    {
        UpdateLevel();
        UpdateStats();
        UpdateButtonState();
        UpdateButtonPrice();
    }

    //Model
    private void OnMoneyChanged(int money)
    {
        UpdateButtonState();
    }

    //Model
    //private void OnLanguageChanged(SystemLanguage system)
    //{
    //    UpdateTitle();
    //}

    //UI
    private void OnButtonClicked()
    {
        if (_upgradesManager.CanLevelUp(_upgrade))
        {
            _upgradesManager.LevelUp(_upgrade);
        }
    }

    private void UpdateTitle()
    {
        //var localizationKey = this.upgrade.Metadata.localizedTitle;
        // var language = LanguageManager.CurrentLanguage;
        //var text = LocalizationManager.GetText(localizationKey, language);

        var text = _upgrade.Metadata.Title;
        _view.SetTitle(text); //TODO... Localization
    }

    private void UpdateIcon()
    {
        _view.SetIcon(_upgrade.Metadata.Icon);
    }

    private void UpdateLevel()
    {
        var text = $"Level: {_upgrade.UpgradeLevel}/{_upgrade.MaxUpgradeLevel}";
        _view.SetLevel(text);
    }

    private void UpdateStats()
    {
        var text = $"Value: {_upgrade.CurrentStats}";
        if (!_upgrade.IsMaxUpgradeLevel)
        {
            text += $" (+{_upgrade.NextImprovement})";
        }

        _view.SetStats(text);
    }

    private void UpdateButtonPrice()
    {
        if (!_upgrade.IsMaxUpgradeLevel)
        {
            var price = _upgrade.NextPrice.ToString();
            _view.UpgradeButton.SetPrice(price);
        }
    }

    private void UpdateButtonState()
    {
        if (_upgrade.IsMaxUpgradeLevel || (_upgrade.UpgradeLevel >= _upgradesManager.CurrentMaxLevelOnHero))
        {
            _view.UpgradeButton.SetState(UpgradeButton.State.MAX);
            return;
        }

        if (_moneyStorage.Money >= _upgrade.NextPrice)
        {
            _view.UpgradeButton.SetState(UpgradeButton.State.AVAILABLE);
        }
        else
        {
            _view.UpgradeButton.SetState(UpgradeButton.State.LOCKED);
        }
    }
}