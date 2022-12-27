using Sirenix.OdinInspector;
using UnityEngine;


public sealed class SoundBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private SoundCatalog catalog;

    [Button]
    [GUIColor(0, 1, 0)]
    public void PlaySound(string id)
    {
        if (this.catalog.FindSound(id, out var sound))
        {
            this.source.PlayOneShot(sound);
        }
    }
}