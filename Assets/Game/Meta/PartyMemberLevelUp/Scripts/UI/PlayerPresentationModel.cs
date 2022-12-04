using Entities;
using System;
using UnityEngine;
public sealed class PlayerPresentationModel : PlayerPopup.IPresentationModel
{
    public event Action<bool> OnLevelUpButtonStateChanged;

    private PartyMember _player;

    private PlayerLevelUpper _playerLevelUpper;

    private IEntity _character;

    public PlayerPresentationModel(PartyMember player, IEntity character, PlayerLevelUpper playerLevelUpper)
    {
        _player = player;
        _character = character;
        _playerLevelUpper = playerLevelUpper;
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

    public string GetHeroName()
    {
        return _player.NameHeroText; 
    }

    public Sprite GetIcon()
    {
        return _player.IconHeroImage;
    }

    public string GetHeroClass()
    {
        return _player.ClassHero.ToString();
    }

    public string GetHeroHistory()
    {
        return _player.HistoryHeroText;
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