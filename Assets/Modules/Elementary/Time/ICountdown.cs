using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    public interface ICountdown
    {
        event Action OnStarted;

        event Action OnTimeChanged;

        event Action OnStopped;

        event Action OnEnded;

        event Action OnReset;

        bool IsPlaying { get; }

        float Progress { get; set; }

        float Duration { get; set; }

        float RemainingTime { get; set; }

        void Play();

        void Stop();

        void ResetTime();
    }
}