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

    private int _maxhitPoints;
    public void Construct(GameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        _maxhitPoints = _character.Get<IComponent_GetHitPoints>().MaxHitPoints;

        SetupPanel();        
    }
    public void OnStartGame()
    {
        _character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged += UpdatePanel;
    }

    public void OnFinishGame()
    {
        _character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged -= UpdatePanel;
    }    

    private void SetupPanel()
    {
        var curhitPoints = _character.Get<IComponent_GetHitPoints>().CurHitPoints;
        _panel.SetupValue($"{curhitPoints} / {_maxhitPoints}");
    }

    private void UpdatePanel(int newHitPoints)
    {
        _panel.UpdateValue($"{newHitPoints} / {_maxhitPoints}");
    }
}
