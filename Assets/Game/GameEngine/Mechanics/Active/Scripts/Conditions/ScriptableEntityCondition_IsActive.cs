using Entities;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Condition «Is Active»",
    menuName = "GameEngine/Mechanics/New Entity Condition «Is Active»"
)]
public sealed class ScriptableEntityCondition_IsActive : ScriptableEntityCondition
{
    public override bool IsTrue(IEntity entity)
    {
        if (entity.TryGet(out IComponent_Active component))
        {
            return component.IsActive;
        }

        Debug.LogWarning("Component «Active» is not found!");
        return default;
    }
}