using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Mechanics/Event Mechanics «Play Audio One Shot»")]
    public sealed class MonoEventMechanics_PlayAudioOneShot : MonoEventMechanics
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
}