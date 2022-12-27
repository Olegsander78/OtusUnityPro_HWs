using Entities;
using UnityEngine;

namespace Game.GameEngine.Mechanics
{
    [CreateAssetMenu(
        fileName = "Condition «Is Alive»",
        menuName = "GameEngine/Mechanics/New Entity Condition «Is Alive»"
    )]
    public sealed class ScriptableEntityCondition_IsAlive : ScriptableEntityCondition
    {
        public override bool IsTrue(IEntity entity)
        {
            if (entity.TryGet(out IComponent_IsAlive component))
            {
                return component.IsAlive;
            }

            Debug.LogWarning("Component «Is Alive» is not found!");
            return default;
        }
    }
}