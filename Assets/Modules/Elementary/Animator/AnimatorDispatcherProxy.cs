using System;
using UnityEngine;

namespace Elementary
{
    /// <summary>
    ///     Receives animation events from animator
    /// </summary>
    [AddComponentMenu("Elementary/Animator/Animator Dispatcher «Proxy»")]
    public sealed class AnimatorDispatcherProxy : AnimatorDispatcherBase
    {
        public override event Action OnEventReceived
        {
            add { this.receiver.OnEventReceived += value; }
            remove { this.receiver.OnEventReceived -= value; }
        }

        public override event Action<bool> OnBoolReceived
        {
            add { this.receiver.OnBoolReceived += value; }
            remove { this.receiver.OnBoolReceived -= value; }
        }

        public override event Action<int> OnIntReceived
        {
            add { this.receiver.OnIntReceived += value; }
            remove { this.receiver.OnIntReceived -= value; }
        }

        public override event Action<float> OnFloatReceived
        {
            add { this.receiver.OnFloatReceived += value; }
            remove { this.receiver.OnFloatReceived -= value; }
        }

        public override event Action<string> OnStringReceived
        {
            add { this.receiver.OnStringReceived += value; }
            remove { this.receiver.OnStringReceived -= value; }
        }

        public override event Action<object> OnObjectReceived
        {
            add { this.receiver.OnObjectReceived += value; }
            remove { this.receiver.OnObjectReceived -= value; }
        }

        [Space]
        [SerializeField]
        private AnimatorDispatcherBase receiver;
    }
}