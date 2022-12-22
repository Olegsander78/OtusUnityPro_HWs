using System;
using System.Collections.Generic;
using UnityEngine;


public sealed class GameAudioChannel : MonoBehaviour
{
    public bool IsEnabled
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

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float volume;

    [Space]
    [SerializeField]
    private AudioSource source;

    private readonly List<IListener> observers = new();

    public void PlaySound(AudioClip clip)
    {
        if (this.isEnable)
        {
            this.source.PlayOneShot(clip);
        }
    }

    private void SetEnable(bool enable)
    {
        if (this.isEnable == enable)
        {
            return;
        }

        this.isEnable = enable;
        this.source.enabled = enable;

        for (int i = 0, count = this.observers.Count; i < count; i++)
        {
            var observer = this.observers[i];
            observer.OnEnabled(enabled);
        }
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

        for (int i = 0, count = this.observers.Count; i < count; i++)
        {
            var observer = this.observers[i];
            observer.OnVolumeChanged(volume);
        }
    }

    public void AddListener(IListener listener)
    {
        this.observers.Add(listener);
    }

    public void RemoveListener(IListener listener)
    {
        this.observers.Remove(listener);
    }

    private void Awake()
    {
        this.source.enabled = this.isEnable;
        this.source.volume = this.volume;
    }

    public interface IListener
    {
        void OnEnabled(bool enabled);

        void OnVolumeChanged(float volume);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        try
        {
            this.Awake();
        }
        catch (Exception)
        {
        }
    }
#endif
}