using Elementary;
using UnityEngine;


public sealed class ActionBehaviour_DisableToggle : ActionBehaviour
{
    [SerializeField]
    private BoolBehaviour toggle;

    public override void Do()
    {
        this.toggle.AssignFalse();
    }
}