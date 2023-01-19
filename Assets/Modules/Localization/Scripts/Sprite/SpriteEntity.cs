using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LocalizationModule
{
    [Serializable]
    public struct SpriteEntity
    {
        [SerializeField]
        public string key;

        [SerializeField]
        public LocalizedSprite[] translations;
    }
}