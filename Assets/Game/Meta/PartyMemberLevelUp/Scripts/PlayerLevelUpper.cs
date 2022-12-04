using UnityEngine;
using Entities;
using Sirenix.OdinInspector;

public class PlayerLevelUpper : MonoBehaviour
{
    [SerializeField]
    private PartyMember _partyMember;

    private ExperienceStorage _experienceStorage;

    private IEntity _character;

    private IComponent_AddLevel _addLevelComponent;

    private IComponent_GetLevel _getLevelComponent;

    private IComponent_OnLevelChanged _onLevelChangedComponent;

    private IComponent_GetExperience _getExperienceComponent;

    private IComponent_ChangeExperience _changeExperienceComponent;

    //public void Construct(GameContext context)
    //{
    //    //_experienceStorage = context.GetService<ExperienceStorage>();

    //    _character = context.GetService<HeroService>().GetHero();
    //    _addLevelComponent = _character.Get<IComponent_AddLevel>();
    //    _getLevelComponent = _character.Get<IComponent_GetLevel>();
    //    _onLevelChangedComponent = _character.Get<IComponent_OnLevelChanged>();
    //    _getExperienceComponent = _character.Get<IComponent_GetExperience>();
    //    _changeExperienceComponent = _character.Get<IComponent_ChangeExperience>();
    //    Debug.Log("construct in PlUpper");

    //}

    [GUIColor(0, 1, 0)]
    [Button]
    public bool CanLevelUp(PartyMember partyMember)
    {
        Debug.Log($"Check Canlevelup {partyMember.MemberOfParty.Get<IComponent_GetExperience>().CurrentExperience} / {partyMember.MemberOfParty.Get<IComponent_GetExperience>().ToNextLevelExperience} ");
        return partyMember.MemberOfParty.Get<IComponent_GetExperience>().CurrentExperience >= partyMember.MemberOfParty.Get<IComponent_GetExperience>().ToNextLevelExperience;
    }


    [GUIColor(0, 1, 0)]
    [Button]
    public void LevelUp(PartyMember partyMember)
    {
        if (CanLevelUp(partyMember))
        {
            partyMember.MemberOfParty.Get<IComponent_ChangeExperience>().
                ChangeExperience(partyMember.MemberOfParty.Get<IComponent_GetExperience>().CurrentExperience);

            Debug.Log($"<color=green>Party member {partyMember.NameHeroText} successfully increased level!</color>");
        }
        else
        {
            Debug.LogWarning($"<color=red>Not enough Exp for party member {partyMember.NameHeroText}!</color>");
        }
    }
}
