using System;
using UnityEngine;

namespace AI.Agents
{
    public abstract class Agent
    {
        public event Action OnStarted;

        public event Action OnStopped;

        public bool IsPlaying
        {
            get { return this.isPlaying; }
        }

        protected bool isPlaying;

        public void Play()
        {
            if (this.isPlaying)
            {
                Debug.LogWarning($"Agent {this.GetType().Name} is already playing!");
                return;
            }

            this.OnStart();
            this.isPlaying = true;
            this.OnStarted?.Invoke();
        }

        protected abstract void OnStart();

        protected abstract void OnStop();

        public void Stop()
        {
            if (!this.isPlaying)
            {
                Debug.LogWarning($"Agent {this.GetType().Name} is not playing!");
                return;
            }

            this.OnStop();
            this.isPlaying = false;
            this.OnStopped?.Invoke();
        }
    }
}