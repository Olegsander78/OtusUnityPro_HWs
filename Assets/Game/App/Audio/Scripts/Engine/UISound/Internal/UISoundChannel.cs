using System;
using UnityEngine;


public sealed class UISoundChannel : MonoBehaviour
{
    public event Action<bool> OnEnabled;

    public event Action<float> OnVolumeChanged;

    public bool IsEnable
    {
        get { return this.isEnable; }
        set { this.SetEnable(value); }
    }

    public float Volume
    {
        get { return this.volume; }
        set { this.SetVolume(value); }
    }

    [SerializeField]
    private bool isEnable;

    [Range(0, 1)]
    [SerializeField]
    private float volume;

    [Space]
    [SerializeField]
    private AudioSource source;

    public void PlaySound(AudioClip clip)
    {
        if (this.isEnable)
        {
            this.source.PlayOneShot(clip);
        }
    }

    private void Awake()
    {
        this.source.volume = this.volume;
        this.source.enabled = this.isEnable;
    }

    private void SetEnable(bool enable)
    {
        if (this.isEnable == enable)
        {
            return;
        }

        this.isEnable = enable;
        this.source.enabled = enable;
        this.OnEnabled?.Invoke(enable);
    }

    private void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        if (Mathf.Approximately(volume, this.volume))
        {
            return;
        }

        this.volume = volume;
        this.source.volume = volume;
        this.OnVolumeChanged?.Invoke(volume);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        try
        {
            this.source.volume = this.volume;
            this.source.enabled = this.isEnable;
        }
        catch (Exception)
        {
        }
    }
#endif
}