using Elementary;
using UnityEngine;


public sealed class MoveInDirectionAction_Base : MoveInDirectionAction
{
    [SerializeField]
    private ActionBehaviour[] actions;

    public override void Do(Vector3 direction)
    {
        for (int i = 0, count = this.actions.Length; i < count; i++)
        {
            var action = this.actions[i];
            action.Do();
        }
    }
}