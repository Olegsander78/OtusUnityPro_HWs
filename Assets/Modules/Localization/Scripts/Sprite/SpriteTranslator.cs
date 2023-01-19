using System.Collections.Generic;
using UnityEngine;

namespace LocalizationModule
{
    public sealed class SpriteTranslator : ITranslator<Sprite>
    {
        private readonly Dictionary<string, SpriteEntity> entities;

        public SpriteTranslator(SpriteEntity[] entities)
        {
            var count = entities.Length;
            this.entities = new Dictionary<string, SpriteEntity>(count);
            
            for (var i = 0; i < count; i++)
            {
                var entity = entities[i];
                this.entities[entity.key] = entity;
            }
        }

        public Sprite GetTranslation(string key, SystemLanguage language)
        {
            if (!this.entities.TryGetValue(key, out var entity))
            {
                return null;
            }

            if (!entity.translations.FindSprite(language, out var result))
            {
                return null;
            }

            return result;
        }
    }
}