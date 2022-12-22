using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "SoundCatalog",
    menuName = "GameEngine/Sounds/New SoundCatalog"
)]
public sealed class SoundCatalog : ScriptableObject
{
    [SerializeField]
    private Sound[] sounds;

    public AudioClip FindSound(string id)
    {
        for (int i = 0, count = this.sounds.Length; i < count; i++)
        {
            var sound = this.sounds[i];
            if (sound.id == id)
            {
                return sound.clip;
            }
        }

        throw new Exception($"Sound is not found {id}");
    }

    public bool FindSound(string id, out AudioClip result)
    {
        for (int i = 0, count = this.sounds.Length; i < count; i++)
        {
            var sound = this.sounds[i];
            if (sound.id == id)
            {
                result = sound.clip;
                return true;
            }
        }

        result = null;
        return false;
    }

    [Serializable]
    private struct Sound
    {
        [SerializeField]
        public string id;

        [SerializeField]
        public AudioClip clip;
    }
}