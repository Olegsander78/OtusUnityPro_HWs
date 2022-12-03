using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerPopup : Popup
{
    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private TextMeshProUGUI _nameHeroText;

    [SerializeField]
    private TextMeshProUGUI _classHeroText;

    [SerializeField]
    private TextMeshProUGUI _historyHeroText;

    [SerializeField]
    private Image _iconHeroImage;

    [SerializeField]
    private TextMeshProUGUI _levelHeroText;

    [SerializeField]
    private TextMeshProUGUI _hitPointsHeroText;

    [SerializeField]
    private TextMeshProUGUI _meleeDamageHeroText;

    [SerializeField]
    private LevelUpButton _levelUpButton;

    private IPresentationModel _presenter;

    protected override void OnShow(object args)
    {
        if (args is not IPresentationModel presenter)
        {
            throw new Exception("Expected Presentation model!");
        }

        _presenter = presenter;
        _presenter.OnLevelUpButtonStateChanged += OnLevelUpButtonStateChanged;
        _presenter.StartPM();

        _titleText.text = presenter.GetTitle();
        _nameHeroText.text = presenter.GetNameHero();
        _classHeroText.text = presenter.GetClassHero();
        _historyHeroText.text = presenter.GetHistoryHero();
        _iconHeroImage.sprite = presenter.GetIcon();
        _levelHeroText.text = presenter.GetLevelHero();
        _hitPointsHeroText.text = presenter.GetMaxHitPointsHero();
        _meleeDamageHeroText.text = presenter.GetMeleeDamageHero();

        _levelUpButton.SetAvailable(presenter.CanLevelUp());
        _levelUpButton.AddListener(OnLevelUpButtonClicked);
    }

    protected override void OnHide()
    {
        _levelUpButton.RemoveListener(OnLevelUpButtonClicked);
        _presenter.OnLevelUpButtonStateChanged -= this.OnLevelUpButtonStateChanged;
        _presenter.StopPM();
    }

    private void OnLevelUpButtonStateChanged(bool isAvailabe)
    {
        _levelUpButton.SetAvailable(isAvailabe);
    }

    private void OnLevelUpButtonClicked()
    {
        _presenter.OnLevelUpClicked();
    }

    public interface IPresentationModel
    {
        event Action<bool> OnLevelUpButtonStateChanged;

        void StartPM();

        void StopPM();

        string GetTitle();

        string GetNameHero();
        string GetClassHero();
        string GetHistoryHero();

        Sprite GetIcon();

        string GetLevelHero();
        string GetMaxHitPointsHero();
        string GetMeleeDamageHero();

        bool CanLevelUp();

        void OnLevelUpClicked();
    }
}