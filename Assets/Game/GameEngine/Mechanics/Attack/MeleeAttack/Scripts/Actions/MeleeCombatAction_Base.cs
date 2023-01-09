using Elementary;
using UnityEngine;


public sealed class MeleeCombatAction_Base : MeleeCombatAction
{
    [SerializeField]
    private ActionBehaviour[] actions;

    public override void Do(MeleeCombatOperation args)
    {
        for (int i = 0, count = this.actions.Length; i < count; i++)
        {
            var action = this.actions[i];
            action.Do();
        }
    }
}