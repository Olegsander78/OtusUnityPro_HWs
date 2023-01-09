using Entities;
using Sirenix.OdinInspector;


public sealed class MeleeCombatOperation
{
    [ReadOnly]
    [ShowInInspector]
    public readonly IEntity targetEntity;

    [ReadOnly]
    [ShowInInspector]
    public bool targetDestroyed;

    public MeleeCombatOperation(IEntity target)
    {
        this.targetEntity = target;
    }
}