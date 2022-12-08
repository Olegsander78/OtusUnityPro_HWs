using Entities;
using UnityEngine;
using GameElements;

public class SpeedParameterAdapter : MonoBehaviour,
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
        _character.Get<IComponent_MoveInDirection>().OnSpeedChanged += UpdatePanel;

    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _character.Get<IComponent_MoveInDirection>().OnSpeedChanged -= UpdatePanel;
    }

    private void SetupPanel()
    {
        var speed = _character.Get<IComponent_MoveInDirection>().Speed;
        _panel.SetupValue(speed.ToString());
    }

    private void UpdatePanel(float newSpeed)
    {
        _panel.UpdateValue(string.Format("{0:0.##}", newSpeed));
    }
}
