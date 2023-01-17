using System;
using UnityEngine;


public sealed class UISoundManager : MonoBehaviour
{
    public static event Action<bool> OnEnabled
    {
        add { instance.channel.OnEnabled += value; }
        remove { instance.channel.OnEnabled -= value; }
    }

    public static event Action<float> OnVolumeChanged
    {
        add { instance.channel.OnVolumeChanged += value; }
        remove { instance.channel.OnVolumeChanged -= value; }
    }

    public static float Volume
    {
        get { return instance.channel.Volume; }
        set { instance.channel.Volume = value; }
    }

    public static bool IsEnable
    {
        get { return instance.channel.IsEnable; }
        set { instance.channel.IsEnable = value; }
    }

    private static UISoundManager instance;

    [Space]
    [SerializeField]
    private UISoundChannel channel;

    [Space]
    [SerializeField]
    private UISoundCatalog catalog;

    public static void PlaySound(AudioClip clip)
    {
        if (!ReferenceEquals(instance, null))
            instance.channel.PlaySound(clip);
    }

    public static void PlaySound(UISoundType type)
    {
        if (ReferenceEquals(instance, null))
            return;

        if (instance.catalog.TryFindClip(type, out AudioClip clip))
        {
            PlaySound(clip);
        }
    }

    private void Awake()
    {
        if (instance != null)
            throw new Exception("UISoundManager is already created!");

        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}