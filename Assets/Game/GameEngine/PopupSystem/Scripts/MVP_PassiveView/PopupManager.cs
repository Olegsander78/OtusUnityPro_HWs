using UIFrames;
using UIFrames.Unity;
using UnityEngine;


public sealed class PopupManager : UnityPopupManager<PopupName>
{
    protected override IFrameSupplier<PopupName, UnityFrame> Supplier
    {
        get { return this.supplier; }
    }

    [SerializeField]
    private PopupSupplier supplier;
}