using System.Collections.Generic;

namespace GameSystem
{
    internal sealed class UpdateContext
    {
        private readonly List<IGameUpdateElement> updateListeners = new();

        private readonly List<IGameFixedUpdateElement> fixedUpdateListeners = new();

        private readonly List<IGameLateUpdateElement> lateUpdateListeners = new();

        public void AddListener(object listener)
        {
            if (listener is IGameUpdateElement updateElement)
            {
                this.updateListeners.Add(updateElement);
            }

            if (listener is IGameFixedUpdateElement fixedUpdateElement)
            {
                this.fixedUpdateListeners.Add(fixedUpdateElement);
            }

            if (listener is IGameLateUpdateElement lateUpdateElement)
            {
                this.lateUpdateListeners.Add(lateUpdateElement);
            }
        }

        public void RemoveListener(object listener)
        {
            if (listener is IGameUpdateElement updateElement)
            {
                this.updateListeners.Remove(updateElement);
            }

            if (listener is IGameFixedUpdateElement fixedUpdateElement)
            {
                this.fixedUpdateListeners.Remove(fixedUpdateElement);
            }

            if (listener is IGameLateUpdateElement lateUpdateElement)
            {
                this.lateUpdateListeners.Remove(lateUpdateElement);
            }
        }
        
        internal void FixedUpdate(float deltaTime)
        {
            for (int i = 0, count = this.fixedUpdateListeners.Count; i < count; i++)
            {
                var listener = this.fixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }

        internal void Update(float deltaTime)
        {
            for (int i = 0, count = this.updateListeners.Count; i < count; i++)
            {
                var listener = this.updateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }

        internal void LateUpdate(float deltaTime)
        {
            for (int i = 0, count = this.lateUpdateListeners.Count; i < count; i++)
            {
                var listener = this.lateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }
    }
}