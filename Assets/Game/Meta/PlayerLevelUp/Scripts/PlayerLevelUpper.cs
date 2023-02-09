using UnityEngine;
using Entities;
using Sirenix.OdinInspector;
using GameSystem;

public class PlayerLevelUpper : MonoBehaviour,
    IGameConstructElement
{
    //private const int INCREMENT_HP_PER_LEVEL = 5;
    //private const float INCREMENT_SPEED_PER_LEVEL = 0.2f; 
    //private const int INCREMENT_MELEEDAMAGE_PER_LEVEL = 1; 
    //private const int INCREMENT_RANGEDAMAGE_PER_LEVEL = 1; 

    private IEntity _character;

    void IGameConstructElement.ConstructGame(GameSystem.IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public bool CanLevelUp()
    {        
        return (_character.Get<IComponent_GetExperience>().CurrentExperience >= _character.Get<IComponent_GetExperience>().ToNextLevelExperience) &&
            (_character.Get<IComponent_GetLevel>().Level <= _character.Get<IComponent_GetLevel>().MaxLevel);
    }


    [GUIColor(0, 1, 0)]
    [Button]
    public void LevelUp()
    {
        if (CanLevelUp())
        {
            _character.Get<IComponent_SpendExperience>().
                SpendExpPerLevel(_character.Get<IComponent_GetExperience>().CurrentExperience);

            //_character.Get<IComponent_SetMeleeDamage>().SetDamage(_character.Get<IComponent_GetMeleeDamage>().Damage + INCREMENT_MELEEDAMAGE_PER_LEVEL);
            //_character.Get<IComponent_ProjectileRangeAttack>().SetDamage(_character.Get<IComponent_ProjectileRangeAttack>().Damage + INCREMENT_RANGEDAMAGE_PER_LEVEL);

            //_character.Get<IComponent_GetHitPoints>().MaxHitPoints += INCREMENT_HP_PER_LEVEL;
            //_character.Get<IComponent_GetHitPoints>().CurHitPoints = _character.Get<IComponent_GetHitPoints>().MaxHitPoints;

            //_character.Get<IComponent_MoveRigidbody>().SetSpeed(_character.Get<IComponent_MoveRigidbody>().Speed + INCREMENT_SPEED_PER_LEVEL);

            Debug.Log($"<color=green>Hero successfully increased level!</color>");
        }
        else
        {
            Debug.LogWarning($"<color=red>Not enough Exp for Hero!</color>");
        }
    }
}
