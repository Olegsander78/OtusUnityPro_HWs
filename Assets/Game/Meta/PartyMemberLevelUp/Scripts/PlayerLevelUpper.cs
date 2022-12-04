using UnityEngine;
using Entities;

public class PlayerLevelUpper : MonoBehaviour, IConstructListener
{
    private ExperienceStorage _experienceStorage;

    private IEntity _character;

    private IComponent_AddLevel _addLevelComponent;

    private IComponent_GetLevel _getLevelComponent;

    private IComponent_OnLevelChanged _onLevelChangedComponent;

    private IComponent_GetExperience _getExperienceComponent;
    public void Construct(GameContext context)
    {
        _experienceStorage = context.GetService<ExperienceStorage>();

        _character = context.GetService<HeroService>().GetHero();
        _addLevelComponent = _character.Get<IComponent_AddLevel>();
        _getLevelComponent = _character.Get<IComponent_GetLevel>();
        _onLevelChangedComponent = _character.Get<IComponent_OnLevelChanged>();
        _getExperienceComponent = _character.Get<IComponent_GetExperience>();
    }

    public bool CanLevelUp()
    {
        return true;  //_experienceStorage.Experience >= 
    }

    

    public void LevelUp()
    {
        
    }
}
