using System;
using UnityEngine;


public sealed class GameAudioManager : MonoBehaviour
{
    public static event Action OnInitialized;

    public static bool IsInitialized
    {
        get { return instanceExists; }
    }

    private static GameAudioManager instance;

    private static bool instanceExists;

    [SerializeField]
    private Holder[] channels;

    public static void PlaySound(GameAudioType channelType, AudioClip sound)
    {
        if (!instanceExists)
            return;

        var channel = instance.FindChannel(channelType);
        channel.PlaySound(sound);
    }

    public static bool IsEnable(GameAudioType type)
    {
        if (!instanceExists)
            return default;

        var channel = instance.FindChannel(type);
        return channel.IsEnabled;
    }

    public static void SetEnable(GameAudioType type, bool enabled)
    {
        if (!instanceExists)
            return;

        var channel = instance.FindChannel(type);
        channel.IsEnabled = enabled;
    }

    public static void SetEnableAll(bool enabled)
    {
        if (!instanceExists)
            return;

        var holders = instance.channels;
        for (int i = 0, count = holders.Length; i < count; i++)
        {
            var holder = holders[i];
            holder.channel.IsEnabled = enabled;
        }
    }

    public static float GetVolume(GameAudioType type)
    {
        if (!instanceExists)
            return default;

        var channel = instance.FindChannel(type);
        return channel.Volume;
    }

    public static void SetVolume(GameAudioType type, float volume)
    {
        if (!instanceExists)
            return;

        var channel = instance.FindChannel(type);
        channel.Volume = volume;
    }

    public static void SetVolumeAll(float volume)
    {
        if (!instanceExists)
            return;

        var holders = instance.channels;
        for (int i = 0, count = holders.Length; i < count; i++)
        {
            var holder = holders[i];
            holder.channel.Volume = volume;
        }
    }

    public static void AddListener(GameAudioType type, GameAudioChannel.IListener listener)
    {
        if (!instanceExists)
            return;

        var channel = instance.FindChannel(type);
        channel.AddListener(listener);
    }

    public static void RemoveListener(GameAudioType type, GameAudioChannel.IListener listener)
    {
        if (!instanceExists)
            return;

        var channel = instance.FindChannel(type);
        channel.RemoveListener(listener);
    }

    private GameAudioChannel FindChannel(GameAudioType channelType)
    {
        for (int i = 0, count = this.channels.Length; i < count; i++)
        {
            var holder = this.channels[i];
            if (holder.channelType == channelType)
            {
                return holder.channel;
            }
        }

        throw new Exception($"Channel of type {channelType} is not found!");
    }

    private void OnEnable()
    {
        if (instanceExists)
            throw new Exception("GameAudioSystem is already exists!");

        instance = this;
        instanceExists = true;
        OnInitialized?.Invoke();
    }

    private void OnDisable()
    {
        instanceExists = false;
        instance = null;
    }

    [Serializable]
    public sealed class Holder
    {
        [SerializeField]
        public GameAudioType channelType;

        [SerializeField]
        public GameAudioChannel channel;
    }
}