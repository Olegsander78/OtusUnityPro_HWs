using System;

namespace Elementary
{
    public interface IStopwatch
    {
        event Action OnStarted;

        event Action OnTimeChanged;

        event Action OnCanceled;

        event Action OnReset;

        bool IsPlaying { get; }

        float CurrentTime { get; set; }

        void Play();

        void Stop();

        void ResetTime();
    }
}