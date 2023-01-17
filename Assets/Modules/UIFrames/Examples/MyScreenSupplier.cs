using UIFrames.Unity;
using UnityEngine;

namespace UIFrames.Examples
{
    public sealed class MyScreenSupplier : UnityFrameSupplier<MyScreenName, UnityFrame>
    {
        [SerializeField]
        private MyScreenFactory factory;
        
        protected override UnityFrame InstantiateFrame(MyScreenName key)
        {
            return this.factory.CreateFrame(key);
        }
    }
}