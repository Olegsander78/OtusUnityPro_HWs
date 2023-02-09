using UnityEngine;

namespace GameSystem
{
    public abstract class ConstructTask : ScriptableObject, IConstructTask
    {
        public abstract void Construct(IGameContext gameContext);
    }
}