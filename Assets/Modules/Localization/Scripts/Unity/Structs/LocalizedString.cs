using System;
using UnityEngine;

namespace LocalizationModule
{
    [Serializable]
    public struct LocalizedString
    {
        [SerializeField]
        public SystemLanguage language;

        [TextArea(minLines: 0, maxLines: 5)]
        [SerializeField]
        public string value;
    }
}