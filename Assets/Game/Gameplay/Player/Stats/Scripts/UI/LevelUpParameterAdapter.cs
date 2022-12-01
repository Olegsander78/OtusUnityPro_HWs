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

    private int _maxLevel;

    public void Construct(GameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        _maxLevel = _character.Get<IComponent_GetLevel>().MaxLevel;

        SetupPanel();        
    }
    public void OnStartGame()
    {
        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged += UpdatePanel;
    }

    public void OnFinishGame()
    {
        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged -= UpdatePanel;
    }    

    private void SetupPanel()
    {
        var curLevel = _character.Get<IComponent_GetLevel>().Level;
        _panel.SetupValue($"{curLevel} / {_maxLevel}");
    }

    private void UpdatePanel(int newLevel)
    {
        _panel.UpdateValue($"{newLevel} / {_maxLevel}");
    }
}
