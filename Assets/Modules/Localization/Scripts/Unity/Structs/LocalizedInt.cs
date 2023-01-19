using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LocalizationModule
{
    [Serializable]
    public struct LocalizedInt
    {
        [SerializeField]
        public SystemLanguage language;

        [SerializeField]
        public int value;
    }
}