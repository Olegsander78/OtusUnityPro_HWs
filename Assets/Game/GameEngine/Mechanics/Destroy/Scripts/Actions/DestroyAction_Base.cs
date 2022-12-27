using Elementary;
using UnityEngine;


public sealed class DestroyAction_Base : DestroyAction
{
    [SerializeField]
    private ActionBehaviour[] actions;

    public override void Do(DestroyEvent destroyEvent)
    {
        for (int i = 0, count = this.actions.Length; i < count; i++)
        {
            var action = this.actions[i];
            action.Do();
        }
    }
}