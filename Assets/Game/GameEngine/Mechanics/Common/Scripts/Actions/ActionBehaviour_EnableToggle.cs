using Elementary;
using UnityEngine;


public sealed class ActionBehaviour_EnableToggle : ActionBehaviour
{
    [SerializeField]
    private BoolBehaviour toggle;

    public override void Do()
    {
        this.toggle.AssignTrue();
    }
}