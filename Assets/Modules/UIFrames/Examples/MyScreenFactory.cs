using System;
using UIFrames.Unity;
using UnityEngine;

namespace UIFrames.Examples
{
    public sealed class MyScreenFactory : UnityFrameFactory<MyScreenName, UnityFrame>
    {
        [SerializeField]
        private ScreenInfo[] screens;

        protected override UnityFrame GetPrefab(MyScreenName key)
        {
            foreach (var frame in this.screens)
            {
                if (frame.name == key)
                {
                    return frame.prefab;
                }
            }

            throw new Exception($"Frame {key} is not found!");
        }


        [Serializable]
        private sealed class ScreenInfo
        {
            [SerializeField]
            public MyScreenName name;

            [SerializeField]
            public UnityFrame prefab;
        }
    }
}