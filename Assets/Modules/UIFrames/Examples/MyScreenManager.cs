using UIFrames.Unity;
using UnityEngine;

namespace UIFrames.Examples
{
    public sealed class MyScreenManager : UnityScreenManager<MyScreenName>
    {
        protected override IFrameSupplier<MyScreenName, UnityFrame> Supplier
        {
            get { return this.supplier; }
        }

        [SerializeField]
        private MyScreenSupplier supplier;
    }
}