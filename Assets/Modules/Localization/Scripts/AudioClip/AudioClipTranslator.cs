using System.Collections.Generic;
using UnityEngine;

namespace LocalizationModule
{
    public sealed class AudioClipTranslator : ITranslator<AudioClip>
    {
        private readonly Dictionary<string, AudioClipEntity> entities;

        public AudioClipTranslator(AudioClipEntity[] entities)
        {
            var count = entities.Length;
            this.entities = new Dictionary<string, AudioClipEntity>(count);
            
            for (var i = 0; i < count; i++)
            {
                var entity = entities[i];
                this.entities[entity.key] = entity;
            }
        }

        public AudioClip GetTranslation(string key, SystemLanguage language)
        {
            if (!this.entities.TryGetValue(key, out var entity))
            {
                return null;
            }

            if (!entity.translations.FindAudioClip(language, out var result))
            {
                return null;
            }

            return result;
        }
    }
}