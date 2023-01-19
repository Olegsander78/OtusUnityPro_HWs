using System.Collections.Generic;
using UnityEngine;

namespace LocalizationModule
{
    public sealed class TextTranslator : ITranslator<string>
    {
        private readonly Dictionary<string, TextEntity> entities;

        public TextTranslator(TextEntity[] entities)
        {
            var count = entities.Length;
            this.entities = new Dictionary<string, TextEntity>(count);
            
            for (var i = 0; i < count; i++)
            {
                var entity = entities[i];
                this.entities[entity.key] = entity;
            }
        }

        public string GetTranslation(string key, SystemLanguage language)
        {
            if (!this.entities.TryGetValue(key, out var entity))
            {
                Debug.LogWarning($"Translation {key} is not found!");
                return null;
            }

            if (!entity.translations.FindString(language, out var result))
            {
                Debug.LogWarning($"Translation {key} with language {language} is not found!");
                return null;
            }

            return result;
        }
        
    }
}