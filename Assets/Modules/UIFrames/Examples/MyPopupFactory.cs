using System;
using UIFrames.Unity;
using UnityEngine;

namespace UIFrames.Examples
{
    public sealed class MyPopupFactory : UnityFrameFactory<MyPopupName, UnityFrame>
    {
        [SerializeField]
        private PopupInfo[] popups;

        protected override UnityFrame GetPrefab(MyPopupName key)
        {
            foreach (var info in this.popups)
            {
                if (info.name == key)
                {
                    return info.prefab;
                }
            }

            throw new Exception($"Frame {key} is not found!");
        }

        [Serializable]
        private sealed class PopupInfo
        {
            [SerializeField]
            public MyPopupName name;

            [SerializeField]
            public UnityFrame prefab;
        }
    }
}