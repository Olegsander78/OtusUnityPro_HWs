using Entities;
using UnityEngine;

public class LevelUpParameterAdapter : MonoBehaviour,
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
        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged += UpdateCurLvlPanel;
        _character.Get<IComponent_OnLevelChanged>().OnMaxLevelChanged += UpdateMaxLvlPanel;
    }

    public void OnFinishGame()
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
