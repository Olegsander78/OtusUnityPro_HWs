using System;
using UnityEngine;

namespace LocalizationModule
{
    [Serializable]
    public struct TextEntity
    {
        [SerializeField]
        public string key;

        [SerializeField]
        public LocalizedString[] translations;
    }
}