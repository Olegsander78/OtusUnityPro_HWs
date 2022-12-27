using UnityEngine;


public sealed class GameAudioObserver : MonoBehaviour, GameAudioChannel.IListener
{
    [SerializeField]
    private GameAudioType channelType;

    [Space]
    [SerializeField]
    private AudioSource[] audioSources;

    private void OnEnable()
    {
        if (GameAudioManager.IsInitialized)
        {
            this.Initialize();
        }
        else
        {
            GameAudioManager.OnInitialized += this.Initialize;
        }
    }

    private void OnDisable()
    {
        GameAudioManager.RemoveListener(this.channelType, this);
    }

    private void Initialize()
    {
        GameAudioManager.OnInitialized -= this.Initialize;
        GameAudioManager.AddListener(this.channelType, this);
        this.SetEnable(GameAudioManager.IsEnable(this.channelType));
        this.SetVolume(GameAudioManager.GetVolume(this.channelType));
    }


    void GameAudioChannel.IListener.OnEnabled(bool enabled)
    {
        this.SetEnable(enabled);
    }

    void GameAudioChannel.IListener.OnVolumeChanged(float volume)
    {
        this.SetVolume(volume);
    }


    private void SetEnable(bool enabled)
    {
        for (int i = 0, count = this.audioSources.Length; i < count; i++)
        {
            var source = this.audioSources[i];
            source.enabled = enabled;
        }
    }

    private void SetVolume(float volume)
    {
        for (int i = 0, count = this.audioSources.Length; i < count; i++)
        {
            var source = this.audioSources[i];
            source.volume = volume;
        }
    }
}