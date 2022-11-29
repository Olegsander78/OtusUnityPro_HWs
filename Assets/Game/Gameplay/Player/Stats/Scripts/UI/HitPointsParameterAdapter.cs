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
        _character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged += UpdatePanel;
    }

    public void OnFinishGame()
    {
        _character.Get<IComponent_OnHitPointsChanged>().OnHitPointsChanged -= UpdatePanel;
    }    

    private void SetupPanel()
    {
        var hitPoints = _character.Get<IComponent_GetHitPoints>().HitPoints;
        _panel.SetupValue(hitPoints.ToString());
    }

    private void UpdatePanel(int newHitPoints)
    {
        _panel.UpdateValue(newHitPoints.ToString());
    }
}
