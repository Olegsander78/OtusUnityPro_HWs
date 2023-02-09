using Entities;
using UnityEngine;
using GameSystem;

public class SpeedParameterAdapter : MonoBehaviour,
    IGameConstructElement,
    IGameStartElement,
    IGameFinishElement

{
    [SerializeField]
    private PropertyPanel _panel;

    private IEntity _character;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        SetupPanel();
    }
    void IGameStartElement.StartGame()
    {
        _character.Get<IComponent_OnMoveSpeedChanged>().OnSpeedChanged += UpdatePanel;

    }

    void IGameFinishElement.FinishGame()
    {
        _character.Get<IComponent_OnMoveSpeedChanged>().OnSpeedChanged -= UpdatePanel;
    }

    private void SetupPanel()
    {
        var speed = _character.Get<IComponent_GetMoveSpeed>().Speed;
        _panel.SetupValue(speed.ToString());
    }

    private void UpdatePanel(float newSpeed)
    {
        _panel.UpdateValue(string.Format("{0:0.##}", newSpeed));
    }
}
