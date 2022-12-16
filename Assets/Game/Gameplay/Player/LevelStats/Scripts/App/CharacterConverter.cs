using Entities;

public static class CharacterConverter
{
    public static void SetupStats(IEntity character, CharacterData data)
    {
        character.Get<IComponent_SetHitPoints>().SetHitPoints(data.currentHitPoints);
        character.Get<IComponent_SetHitPoints>().SetHitPoints(data.maxHitPoints);
        character.Get<IComponent_SetMeleeDamage>().SetDamage(data.meleeDamage);
        character.Get<IComponent_ProjectileRangeAttack>().SetDamage(data.rangeDamage);
        character.Get<IComponent_MoveRigidbody>().SetSpeed(data.speed);
        character.Get<IComponent_SetLevel>().SetLevel(data.currentLevel);
        character.Get<IComponent_SetLevel>().SetMaxLevel(data.maxLevel);
        character.Get<IComponent_SetExperience>().SetCurrentExperience(data.currentExp);
        character.Get<IComponent_SetExperience>().SetNextLevelExperience(data.nextLvlExp);
        character.Get<IComponent_SetExperience>().SetTotalExperience(data.totalExp);
    }

    public static CharacterData ConvertToData(IEntity character)
    {
        return new CharacterData
        {
            currentHitPoints = character.Get<IComponent_GetHitPoints>().CurHitPoints,
            maxHitPoints = character.Get<IComponent_GetHitPoints>().MaxHitPoints,
            meleeDamage = character.Get<IComponent_GetMeleeDamage>().Damage,
            rangeDamage = character.Get<IComponent_ProjectileRangeAttack>().Damage,
            speed = character.Get<IComponent_MoveRigidbody>().Speed,
            currentLevel = character.Get<IComponent_GetLevel>().Level,
            maxLevel = character.Get<IComponent_GetLevel>().MaxLevel,
            currentExp = character.Get<IComponent_GetExperience>().CurrentExperience,
            nextLvlExp = character.Get<IComponent_GetExperience>().ToNextLevelExperience,
            totalExp = character.Get<IComponent_GetExperience>().TotalExperience
        };
    }
}