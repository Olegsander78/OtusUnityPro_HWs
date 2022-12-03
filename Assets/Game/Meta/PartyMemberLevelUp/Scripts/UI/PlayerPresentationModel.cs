using System;
using UnityEngine;


public sealed class PlayerPresentationModel : PlayerPopup.IPresentationModel
{
    public event Action<bool> OnLevelUpButtonStateChanged;

    private readonly PartyMember _player;

    private readonly PlayerLevelUpper _playerLevelUpper;

    private readonly ExperienceStorage _expStorage;

    public PlayerPresentationModel(PartyMember player, PlayerLevelUpper playerLevelUpper, ExperienceStorage expStorage)
    {
        _player = player;
        _playerLevelUpper = playerLevelUpper;
        _expStorage = expStorage;
    }

    public void StartPM()
    {
        //_expStorage.OnExpChanged += OnExpChanged;
    }

    public void StopPM()
    {
        //_expStorage.OnExpChanged -= OnExpChanged;
    }

    public string GetTitle()
    {
        return _player.TitleText;
    }

    public string GetNameHero()
    {
        return _player.NameHeroText;    }

    public Sprite GetIcon()
    {
        return _player.IconHeroImage;
    }

    public string GetClassHero()
    {
        return _player.ClassHero.ToString();
    }

    public string GetHistoryHero()
    {
        return _player.HistoryHeroText;
    }

    public string GetLevelHero()
    {
        return _player.MemberOfParty.Get<IComponent_GetLevel>().Level.ToString();
    }

    public string GetMaxHitPointsHero()
    {
        return _player.MemberOfParty.Get<IComponent_GetHitPoints>().MaxHitPoints.ToString();
    }

    public string GetMeleeDamageHero()
    {
        return _player.MemberOfParty.Get<IComponent_GetMeleeDamage>().Damage.ToString();
    }

    public bool CanLevelUp()
    {
        // return _playerLevelUpper.CanLevelUp(_player);
        return true;
    }

    public void OnLevelUpClicked()
    {
        //_playerLevelUpper.LevelUp(_player);
    }

    private void OnExpChanged(int money)
    {
       // var canLevelUp = _playerLevelUpper.CanLevelUp(_player);
       // OnLevelUpButtonStateChanged?.Invoke(canLevelUp);
    }    
}