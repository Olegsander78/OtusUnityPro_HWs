using Elementary;
using UnityEngine;


public abstract class HarvestResourceAction : MonoBehaviour, IAction<HarvestResourceOperation>
{
    public abstract void Do(HarvestResourceOperation operation);
}