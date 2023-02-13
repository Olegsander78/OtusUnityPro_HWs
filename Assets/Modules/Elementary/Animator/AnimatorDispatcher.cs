using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    /// <summary>
    ///     Receives animation events from animator
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Elementary/Animator/Animator Dispatcher")]
    public sealed class AnimatorDispatcher : AnimatorDispatcherBase
    {
        public override event Action OnEventReceived;

        public override event Action<bool> OnBoolReceived;

        public override event Action<int> OnIntReceived;

        public override event Action<float> OnFloatReceived;

        public override event Action<string> OnStringReceived;

        public override event Action<object> OnObjectReceived;

        [PropertySpace]
        [GUIColor(0, 1, 0)]
        [Button]
        public void ReceiveString(string message)
        {
            this.OnStringReceived?.Invoke(message);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void ReceiveBool(bool message)
        {
            this.OnBoolReceived?.Invoke(message);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void ReceiveInt(int message)
        {
            this.OnIntReceived?.Invoke(message);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void ReceiveFloat(float message)
        {
            this.OnFloatReceived?.Invoke(message);
        }

        public void ReceiveObject(object obj)
        {
            this.OnObjectReceived?.Invoke(obj);
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public void ReceiveEvent()
        {
            this.OnEventReceived?.Invoke();
        }
    }
}