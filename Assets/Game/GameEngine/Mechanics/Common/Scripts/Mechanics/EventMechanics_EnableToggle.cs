using Elementary;
using UnityEngine;


public sealed class EventMechanics_EnableToggle : EventMechanics
{
    [SerializeField]
    private bool setEnable = true;

    [SerializeField]
    private BoolBehaviour toggle;

    protected override void OnEvent()
    {
        this.toggle.Assign(this.setEnable);
    }
}