using UnityEngine;

public class PlayerLevelUpper : MonoBehaviour, IConstructListener
{
    private ExperienceStorage _experienceStorage;
    public void Construct(GameContext context)
    {
        _experienceStorage = context.GetService<ExperienceStorage>();
    }

    public bool CanLevelUp()
    {
        return true;  //_experienceStorage.Experience >= 
    }

    

    public void LevelUp()
    {

    }
}
