using Entities;
using UnityEngine;
using GameElements;

public class HitPointsParameterAdapter : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private PropertyPanel _panel;

    private IEntity _character;
    void IGameInitElement.InitGame(IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();

        SetupPanel();        
    }
    void IGameStartElement.StartGame(IGameContext context)
    {
        _character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged += UpdateCurHPPanel;
        _character.Get<IComponent_OnHitPointsChanged>().OnMaxHitPointsChanged += UpdateMaxHPPanel;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged -= UpdateCurHPPanel;
        _character.Get<IComponent_OnHitPointsChanged>().OnMaxHitPointsChanged -= UpdateMaxHPPanel;
    }    

    private void SetupPanel()
    {
        var curhitPoints = _character.Get<IComponent_GetHitPoints>().CurHitPoints;
        var maxhitPoints = _character.Get<IComponent_GetHitPoints>().MaxHitPoints;

        _panel.SetupValue($"{curhitPoints} / {maxhitPoints}");
    }

    private void UpdateCurHPPanel(int newHitPoints)
    {
        _panel.UpdateValue($"{newHitPoints} / {_character.Get<IComponent_GetHitPoints>().MaxHitPoints}");
    }
    private void UpdateMaxHPPanel(int newMaxHitPoints)
    {
        _panel.UpdateValue($"{_character.Get<IComponent_GetHitPoints>().CurHitPoints} / {newMaxHitPoints}");
    }
}
