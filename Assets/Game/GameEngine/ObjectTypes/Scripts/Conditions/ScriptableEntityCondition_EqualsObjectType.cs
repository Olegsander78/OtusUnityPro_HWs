using Entities;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Condition «Equals Object Type»",
    menuName = "GameEngine/Mechanics/New Entity Condition «Equals Object Type»"
)]
public sealed class ScriptableEntityCondition_EqualsObjectType : ScriptableEntityCondition
{
    [SerializeField]
    private ObjectType objectType;

    public override bool IsTrue(IEntity entity)
    {
        if (entity.TryGet(out IComponent_GetObjectType component))
        {
            return component.ObjectType == this.objectType;
        }

        Debug.LogWarning("Component «Get Object Type» is not found!");
        return default;
    }
}