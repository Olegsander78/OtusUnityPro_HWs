using System;
using TMPro;
using UnityEngine;

namespace LocalizationModule
{
    [Serializable]
    public class TMProUGUIComponent_FontSize : IComponent
    {
        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private LocalizedInt[] fontSizes = new LocalizedInt[0];
        
        public void UpdateLanguage(SystemLanguage language)
        {
            if (this.fontSizes.FindInt(language, out var value))
            {
                this.text.fontSize = value;
            }
        }
    }
}