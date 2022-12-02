using Entities;
using System;
using UnityEngine;


//ADAPTER
public sealed class ExpPanelAdapter : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    [SerializeField]
    private ExperiencePanel _expPanel;

    private ExperienceStorage _expStorage;

    private IEntity _character;

    private Component_LevelUp _component_LevelUp;

    void IConstructListener.Construct(GameContext context)
    {
        _expStorage = context.GetService<ExperienceStorage>();

        _character = context.GetService<HeroService>().GetHero();

        _component_LevelUp = _character.Get<Component_LevelUp>();

        _expPanel.SetupLevel(_component_LevelUp.Level.ToString());

        var maxExpOnLevel = _component_LevelUp.Level * 100 / 2;
        
        _expPanel.SetupExp(_expStorage.Experience.ToString(),maxExpOnLevel.ToString());
    }

    public void OnStartGame()
    {
        _expStorage.OnExpChanged += OnExpChanged;
        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged += UpdateCurLvlPanel;
    }    

    public void OnFinishGame()
    {
        _expStorage.OnExpChanged -= OnExpChanged;
        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged -= UpdateCurLvlPanel;
    }

    private void OnExpChanged(int exp)
    {
        var maxExpOnLevel = _component_LevelUp.Level * 100 / 2;

        _expPanel.UpdateExp(exp.ToString(),maxExpOnLevel.ToString());
    }
    private void UpdateCurLvlPanel(int level)
    {
        _expPanel.UpdateLevel(level.ToString());
    }
}

