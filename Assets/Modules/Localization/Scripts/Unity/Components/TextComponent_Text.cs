using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace LocalizationModule
{
    [Serializable]
    public class TextComponent_Text : IComponent
    {
        [SerializeField]
        private Text text;

        [SerializeField]
        private LocalizedString[] texts = new LocalizedString[0];
        
        public void UpdateLanguage(SystemLanguage language)
        {
            if (this.texts.FindString(language, out var value))
            {
                this.text.text = value;
            }
        }
    }
}