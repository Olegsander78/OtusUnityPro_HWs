using Entities;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Condition «Can Collect»",
    menuName = "GameEngine/Mechanics/New Entity Condition «Can Collect»"
)]
public sealed class ScriptableEntityCondition_CanCollect : ScriptableEntityCondition
{
    public override bool IsTrue(IEntity entity)
    {
        if (entity.TryGet(out IComponent_CanCollect component))
        {
            return component.CanCollect;
        }

        Debug.LogWarning("Component «Can Collect» is not found!");
        return default;
    }
}