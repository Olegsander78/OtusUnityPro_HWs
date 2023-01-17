using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "UISoundCatalog",
    menuName = "Audio/New UISoundCatalog"
)]
public sealed class UISoundCatalog : ScriptableObject
{
    [SerializeField]
    private Sounds[] sounds;

    public bool TryFindClip(UISoundType type, out AudioClip clip)
    {
        for (int i = 0, count = this.sounds.Length; i < count; i++)
        {
            var sound = this.sounds[i];
            if (sound.type == type)
            {
                clip = sound.clip;
                return true;
            }
        }

        clip = null;
        return false;
    }

    public AudioClip FindClip(UISoundType type)
    {
        for (int i = 0, count = this.sounds.Length; i < count; i++)
        {
            var sound = this.sounds[i];
            if (sound.type == type)
            {
                return sound.clip;
            }
        }

        throw new Exception($"Sound {type} is not found!");
    }

    [Serializable]
    private sealed class Sounds
    {
        [SerializeField]
        public UISoundType type;

        [SerializeField]
        public AudioClip clip;
    }
}