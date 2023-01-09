using Elementary;
using UnityEngine;


public sealed class ActionBehaviour_StopMeleeCombat : ActionBehaviour
{
    [SerializeField]
    private MeleeCombatEngine engine;

    public override void Do()
    {
        if (this.engine.IsCombat)
        {
            this.engine.StopCombat();
        }
    }
}