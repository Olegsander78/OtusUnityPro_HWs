#if ODIN_INSPECTOR
using Sirenix.OdinInspector;

#else
using UnityEngine;
#endif

namespace Entities
{
#if ODIN_INSPECTOR
    public abstract class ScriptableEntityCondition : SerializedScriptableObject, IEntityCondition
    {
        public abstract bool IsTrue(IEntity entity);
    }
#else
    public abstract class ScriptableEntityCondition : ScriptableObject, IEntityCondition
    {
        public abstract bool IsTrue(IEntity entity);
    }
#endif
}