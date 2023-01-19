using UnityEngine;

namespace LocalizationModule
{
    public static class Extensions
    {
        public static bool FindSprite(this LocalizedSprite[] sprites, SystemLanguage language, out Sprite value)
        {
            for (int i = 0, count = sprites.Length; i < count; i++)
            {
                var sprite = sprites[i];
                if (sprite.language == language)
                {
                    value = sprite.value;
                    return true;
                }
            }

            value = null;
            return false;
        }

        public static bool FindInt(this LocalizedInt[] integers, SystemLanguage language, out int value)
        {
            for (int i = 0, count = integers.Length; i < count; i++)
            {
                var integer = integers[i];
                if (integer.language == language)
                {
                    value = integer.value;
                    return true;
                }
            }

            value = default;
            return false;
        }
        
        public static bool FindString(this LocalizedString[] strings, SystemLanguage language, out string value)
        {
            for (int i = 0, count = strings.Length; i < count; i++)
            {
                var str = strings[i];
                if (str.language == language)
                {
                    value = str.value;
                    return true;
                }
            }

            value = default;
            return false;
        }
        
        
        public static bool FindAudioClip(this LocalizedAudioClip[] clips, SystemLanguage language, out AudioClip value)
        {
            for (int i = 0, count = clips.Length; i < count; i++)
            {
                var str = clips[i];
                if (str.language == language)
                {
                    value = str.value;
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}