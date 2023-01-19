using System;
using UnityEngine;

namespace LocalizationModule
{
    [Serializable]
    public struct LocalizedAudioClip
    {
        [SerializeField]
        public SystemLanguage language;

        [SerializeField]
        public AudioClip value;
    }
}