using Entities;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Condition «Can Take Damage»",
    menuName = "GameEngine/Mechanics/New Entity Condition «Can Take Damage»"
)]
public sealed class ScriptableEntityCondition_CanTakeDamage : ScriptableEntityCondition
{
    public override bool IsTrue(IEntity entity)
    {
        return entity.TryGet(out IComponent_TakeDamage _);
    }
}