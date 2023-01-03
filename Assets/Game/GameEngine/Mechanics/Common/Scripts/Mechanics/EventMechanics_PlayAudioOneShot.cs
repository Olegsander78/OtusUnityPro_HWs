using UnityEngine;


public sealed class EventMechanics_PlayAudioOneShot : EventMechanics
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    protected override void OnEvent()
    {
        this.audioSource.PlayOneShot(this.audioClip);
    }
}