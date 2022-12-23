using Elementary;
using UnityEngine;


public abstract class DestroyAction : MonoBehaviour, IAction<DestroyEvent>
{
    public abstract void Do(DestroyEvent destroyEvent);
}