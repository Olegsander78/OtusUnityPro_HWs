using Entities;
using UnityEngine;

public class HitPointsParameterAdapter : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    [SerializeField]
    private PropertyPanel _panel;

    private IEntity _character;
    public void Construct(GameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();

        SetupPanel();        
    }
    public void OnStartGame()
    {
        _character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged += UpdateCurHPPanel;
        _character.Get<IComponent_OnHitPointsChanged>().OnMaxHitPointsChanged += UpdateMaxHPPanel;
    }

    public void OnFinishGame()
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
