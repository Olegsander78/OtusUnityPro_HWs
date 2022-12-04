using UnityEngine;
using Entities;
using Sirenix.OdinInspector;

public class PlayerLevelUpper : MonoBehaviour, IConstructListener
{    
    private IEntity _character;

    public void Construct(GameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        Debug.Log("construct in PlUpper");
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public bool CanLevelUp()
    {
        Debug.Log($"Check Canlevelup {_character.Get<IComponent_GetExperience>().CurrentExperience} / {_character.Get<IComponent_GetExperience>().ToNextLevelExperience} ");
        return _character.Get<IComponent_GetExperience>().CurrentExperience >= _character.Get<IComponent_GetExperience>().ToNextLevelExperience;
    }


    [GUIColor(0, 1, 0)]
    [Button]
    public void LevelUp(PartyMember partyMember)
    {
        if (CanLevelUp())
        {
            _character.Get<IComponent_SpendExperience>().
                SpendExpPerLevel(_character.Get<IComponent_GetExperience>().CurrentExperience);

            Debug.Log($"<color=green>Party member {partyMember.NameHeroText} successfully increased level!</color>");
        }
        else
        {
            Debug.LogWarning($"<color=red>Not enough Exp for party member {partyMember.NameHeroText}!</color>");
        }
    }
}
