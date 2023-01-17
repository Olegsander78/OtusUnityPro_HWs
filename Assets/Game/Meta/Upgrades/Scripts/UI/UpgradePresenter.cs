using UnityEngine;


public sealed class UpgradePresenter
{
    private readonly Upgrade upgrade;

    private readonly UpgradeView view;

    private UpgradesManager upgradesManager;

    private MoneyStorage moneyStorage;

    public UpgradePresenter(Upgrade upgrade, UpgradeView view)
    {
        this.upgrade = upgrade;
        this.view = view;
    }

    public void Construct(UpgradesManager upgradesManager, MoneyStorage moneyStorage)
    {
        this.upgradesManager = upgradesManager;
        this.moneyStorage = moneyStorage;
    }

    public void Start()
    {
        this.view.UpgradeButton.AddListener(this.OnButtonClicked);
       // this.upgrade.OnLevelUp += this.OnLevelUp;
        this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
        //LanguageManager.OnLanguageChanged += this.OnLanguageChanged;

        this.UpdateTitle();
        this.UpdateLevel();
        this.UpdateIcon();
        this.UpdateStats();
        this.UpdateButtonPrice();
        this.UpdateButtonState();
    }

    public void Stop()
    {
        this.view.UpgradeButton.RemoveListener(this.OnButtonClicked);
        //this.upgrade.OnLevelUp -= this.OnLevelUp;
        this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
        //LanguageManager.OnLanguageChanged -= this.OnLanguageChanged;
    }

    //Model
    private void OnLevelUp(int newLevel)
    {
        this.UpdateLevel();
        this.UpdateStats();
        this.UpdateButtonState();
        this.UpdateButtonPrice();
    }

    //Model
    private void OnMoneyChanged(int money)
    {
        this.UpdateButtonState();
    }

    //Model
    private void OnLanguageChanged(SystemLanguage system)
    {
        this.UpdateTitle();
    }

    //UI
    private void OnButtonClicked()
    {
        if (this.upgradesManager.CanLevelUp(this.upgrade))
        {
            this.upgradesManager.LevelUp(this.upgrade);
        }
    }

    private void UpdateTitle()
    {
        //var localizationKey = this.upgrade.Metadata.localizedTitle;
       // var language = LanguageManager.CurrentLanguage;
        //var text = LocalizationManager.GetText(localizationKey, language);
        //this.view.SetTitle(text); //TODO... Localization
    }

    private void UpdateIcon()
    {
        //this.view.SetIcon(this.upgrade.Metadata.icon);
    }

    private void UpdateLevel()
    {
        //var text = $"Level: {this.upgrade.Level}/{this.upgrade.MaxLevel}";
        //this.view.SetLevel(text);
    }

    private void UpdateStats()
    {
        //var text = $"Value: {this.upgrade.CurrentStats}";
        //if (!this.upgrade.IsMaxLevel)
        //{
        //    text += $" (+{this.upgrade.NextImprovement})";
        //}

        //this.view.SetStats(text);
    }

    private void UpdateButtonPrice()
    {
        //if (!this.upgrade.IsMaxLevel)
        //{
        //    var price = this.upgrade.NextPrice.ToString();
        //    this.view.Button.SetPrice(price);
        //}
    }

    private void UpdateButtonState()
    {
        //if (this.upgrade.IsMaxLevel)
        //{
        //    this.view.Button.SetState(UpgradeButton.State.MAX);
        //    return;
        //}

        //if (this.moneyStorage.Money >= this.upgrade.NextPrice)
        //{
        //    this.view.Button.SetState(UpgradeButton.State.AVAILABLE);
        //}
        //else
        //{
        //    this.view.Button.SetState(UpgradeButton.State.LOCKED);
        //}
    }
}