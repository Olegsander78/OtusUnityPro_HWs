using UIFrames.Unity;
using UnityEngine;

namespace UIFrames.Examples
{
    public sealed class MyPopupSupplier : UnityFrameSupplier<MyPopupName, UnityFrame>
    {
        [SerializeField]
        private MyPopupFactory factory;

        protected override UnityFrame InstantiateFrame(MyPopupName key)
        {
            return this.factory.CreateFrame(key);
        }
    }
}