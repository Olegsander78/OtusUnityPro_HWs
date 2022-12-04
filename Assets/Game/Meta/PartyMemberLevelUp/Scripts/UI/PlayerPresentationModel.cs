using Entities;
using System;
using UnityEngine;
public sealed class PlayerPresentationModel : PlayerPopup.IPresentationModel
{
    public event Action<bool> OnLevelUpButtonStateChanged;

    private PartyMember _partyMember;

    private PlayerLevelUpper _playerLevelUpper;

    private IEntity _character;

    public PlayerPresentationModel(PartyMember partyMember, IEntity character, PlayerLevelUpper playerLevelUpper)
    {
        _partyMember = partyMember;
        _character = character;
        _playerLevelUpper = playerLevelUpper;
    }

    public void StartPM()
    {
        _character.Get<IComponent_ChangeExperience>().OnExperienceChanged += OnExpSpended;
    }

    public void StopPM()
    {
        _character.Get<IComponent_ChangeExperience>().OnExperienceChanged -= OnExpSpended;
    }

    public string GetTitle()
    {
        return _partyMember.TitleText;
    }

    public string GetHeroName()
    {
        return _partyMember.NameHeroText; 
    }

    public Sprite GetIcon()
    {
        return _partyMember.IconHeroImage;
    }

    public string GetHeroClass()
    {
        return _partyMember.ClassHero.ToString();
    }

    public string GetHeroHistory()
    {
        return _partyMember.HistoryHeroText;
    }

    public string GetHeroLevel()
    {
        return _character.Get<IComponent_GetLevel>().Level.ToString();
    }

    public string GetMaxHitPointsHero()
    {
        return _character.Get<IComponent_GetHitPoints>().MaxHitPoints.ToString();
    }

    public string GetMeleeDamageHero()
    {
        return _character.Get<IComponent_GetMeleeDamage>().Damage.ToString();
    }
    public string GetCurrentExperience()
    {
        return _character.Get<IComponent_GetExperience>().CurrentExperience.ToString();
    }

    public string GetPrice()
    {
        return _character.Get<IComponent_GetExperience>().ToNextLevelExperience.ToString();
    }

    public bool CanLevelUp()
    {
        return _playerLevelUpper.CanLevelUp();        
    }

    public void OnLevelUpClicked()
    {
        _playerLevelUpper.LevelUp(_partyMember);
    }

    private void OnExpSpended(int exp)
    {
        var canLevelUp = _playerLevelUpper.CanLevelUp();
        OnLevelUpButtonStateChanged?.Invoke(canLevelUp);
    }
}