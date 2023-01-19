using System;
using UnityEngine;

namespace LocalizationModule
{
    [Serializable]
    public sealed class SpriteRendererComponent_Sprite : IComponent
    {
        [SerializeField]
        private SpriteRenderer renderer;

        [SerializeField]
        private LocalizedSprite[] sprites = new LocalizedSprite[0];
        
        public void UpdateLanguage(SystemLanguage language)
        {
            if (this.sprites.FindSprite(language, out var sprite))
            {
                this.renderer.sprite = sprite;
            }
        }
    }
}