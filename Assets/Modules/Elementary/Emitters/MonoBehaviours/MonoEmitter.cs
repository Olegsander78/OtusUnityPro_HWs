using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Emitters/Emitter")]
    public sealed class MonoEmitter : MonoBehaviour, IEmitter
    {
        public event Action OnEvent;

        private readonly List<IAction> listeners = new();

        [Button, GUIColor(0, 1, 0)]
        public void Call()
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do();
            }

            this.OnEvent?.Invoke();
        }

        public void AddListener(IAction listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction listener)
        {
            this.listeners.Remove(listener);
        }
    }

    public abstract class MonoEmitter<T> : MonoBehaviour, IEmitter<T>
    {
        public event Action<T> OnEvent;

        private readonly List<IAction> listeners = new();

        private readonly List<IAction<T>> tListeners = new();

        [Button, GUIColor(0, 1, 0)]
        public void Call(T value)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.Do();
            }

            for (int i = 0, count = this.tListeners.Count; i < count; i++)
            {
                var listener = this.tListeners[i];
                listener.Do(value);
            }

            this.OnEvent?.Invoke(value);
        }

        public void AddListener(IAction listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IAction listener)
        {
            this.listeners.Remove(listener);
        }

        public void AddListener(IAction<T> listener)
        {
            this.tListeners.Add(listener);
        }

        public void RemoveListener(IAction<T> listener)
        {
            this.tListeners.Remove(listener);
        }
    }
}