using Entities;
using UnityEngine;
using GameSystem;

public class LevelUpParameterAdapter : MonoBehaviour,
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
        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged += UpdateCurLvlPanel;
        _character.Get<IComponent_OnLevelChanged>().OnMaxLevelChanged += UpdateMaxLvlPanel;
    }

    void IGameFinishElement.FinishGame()
    {
        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged -= UpdateCurLvlPanel;
        _character.Get<IComponent_OnLevelChanged>().OnMaxLevelChanged -= UpdateMaxLvlPanel;
    }    

    private void SetupPanel()
    {
        var curLevel = _character.Get<IComponent_GetLevel>().Level;
        var maxLevel = _character.Get<IComponent_GetLevel>().MaxLevel;

        _panel.SetupValue($"{curLevel} /{maxLevel}");       
    }

    private void UpdateCurLvlPanel(int newLevel)
    {
        _panel.UpdateValue($"{newLevel} /{_character.Get<IComponent_GetLevel>().MaxLevel}");
    }
    private void UpdateMaxLvlPanel(int newMaxLevel)
    {
        _panel.UpdateValue($"{_character.Get<IComponent_GetLevel>().Level} /{newMaxLevel}");
    }
}
