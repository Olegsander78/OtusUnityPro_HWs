using System;
using UnityEngine;

namespace LocalizationModule
{
    [Serializable]
    public struct LocalizedSprite
    {
        [SerializeField]
        public SystemLanguage language;

        [SerializeField]
        public Sprite value;
    }
}