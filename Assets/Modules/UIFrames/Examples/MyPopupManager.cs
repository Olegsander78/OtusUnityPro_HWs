using UIFrames.Unity;
using UnityEngine;

namespace UIFrames.Examples
{
    public sealed class MyPopupManager : UnityPopupManager<MyPopupName>
    {
        protected override IFrameSupplier<MyPopupName, UnityFrame> Supplier
        {
            get { return this.supplier; }
        }

        [SerializeField]
        private MyPopupSupplier supplier;
    }
}