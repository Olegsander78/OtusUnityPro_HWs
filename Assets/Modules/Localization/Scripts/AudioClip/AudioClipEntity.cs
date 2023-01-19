using System;
using UnityEngine;

namespace LocalizationModule
{
    [Serializable]
    public struct AudioClipEntity
    {
        [SerializeField]
        public string key;

        [SerializeField]
        public LocalizedAudioClip[] translations;
    }
}