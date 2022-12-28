using Entities;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Condition «Can Hit»",
    menuName = "GameEngine/Mechanics/New Entity Condition «Can Hit»"
)]
public sealed class ScriptableEntityCondition_CanHit : ScriptableEntityCondition
{
    public override bool IsTrue(IEntity entity)
    {
        return entity.TryGet(out IComponent_Hit _);
    }
}