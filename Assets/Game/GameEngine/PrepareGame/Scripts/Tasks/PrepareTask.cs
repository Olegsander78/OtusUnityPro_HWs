using GameSystem;
using UnityEngine;


public abstract class PrepareTask : ScriptableObject
{
    public abstract void Prepare(IGameContext gameContext);
}