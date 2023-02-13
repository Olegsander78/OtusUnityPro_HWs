using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Elementary
{
    [Serializable]
    public class Emitter : IEmitter
    {
        public event Action OnEvent;

        private readonly List<IAction> listeners = new();

        [GUIColor(0, 1, 0)]
        [Button]
        public virtual void Call()
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

    [Serializable]
    public class Emitter<T> : IEmitter<T>
    {
        public event Action<T> OnEvent;

        private readonly List<IAction> listeners = new();

        private readonly List<IAction<T>> tListeners = new();

        [GUIColor(0, 1, 0)]
        [Button]
        public virtual void Call(T value)
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
            this.listeners?.Remove(listener);
        }

        public void AddListener(IAction<T> listener)
        {
            this.tListeners.Add(listener);
        }

        public void RemoveListener(IAction<T> listener)
        {
            this.tListeners?.Remove(listener);
        }
    }
}