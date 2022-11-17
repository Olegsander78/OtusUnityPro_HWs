using UnityEngine;

namespace Entities
{
    public abstract class UnityEntityCondition : MonoBehaviour, IEntityCondition
    {
        public abstract bool IsTrue(IEntity entity);
    }
}