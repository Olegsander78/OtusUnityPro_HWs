using Entities;
using UnityEngine;
using GameElements;

//ADAPTER
public sealed class ExpPanelAdapter : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private ExperiencePanel _expPanel;

    private IEntity _character;

    [SerializeField]
    private PartyMember _partyMember;

    private IComponent_GetLevel _component_GetLevel;

    private IComponent_GetExperience _component_GetExp;

    private IComponent_ChangeExperience _component_AddExp;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();

        _component_GetLevel = _character.Get<IComponent_GetLevel>();

        _component_GetExp = _character.Get<IComponent_GetExperience>();

        _component_AddExp = _character.Get<IComponent_ChangeExperience>();

        _expPanel.SetupLevel(_component_GetLevel.Level.ToString());
        
        _expPanel.SetupExp(_component_GetExp.CurrentExperience.ToString(),_component_GetExp.ToNextLevelExperience.ToString());

        _expPanel.SetupIcon(_partyMember.IconHeroImage);
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _component_AddExp.OnExperienceChanged += OnCurrentExpChanged;
        _component_AddExp.OnNextlvlExperienceChanged += OnNextLvlExpChanged;

        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged += UpdateCurLvlPanel;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _component_AddExp.OnExperienceChanged -= OnCurrentExpChanged;
        _component_AddExp.OnNextlvlExperienceChanged -= OnNextLvlExpChanged;

        _character.Get<IComponent_OnLevelChanged>().OnLevelChanged -= UpdateCurLvlPanel;
    }

    private void OnCurrentExpChanged(int exp)
    {
        _expPanel.UpdateExp(exp.ToString(), _component_GetExp.ToNextLevelExperience.ToString());
    }
    private void OnNextLvlExpChanged(int exp)
    {
        _expPanel.UpdateExp(_component_GetExp.CurrentExperience.ToString(), exp.ToString());
    }
    private void UpdateCurLvlPanel(int level)
    {
        _expPanel.UpdateLevel(_component_GetLevel.Level.ToString());
    }
}

