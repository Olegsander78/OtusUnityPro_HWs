using System;
using UnityEngine;

namespace Elementary
{
    public abstract class MultiEventReceiver : MonoBehaviour
    {
        public abstract event Action OnEventReceived;

        public abstract event Action<bool> OnBoolReceived;

        public abstract event Action<int> OnIntReceived;

        public abstract event Action<float> OnFloatReceived;

        public abstract event Action<string> OnStringReceived;

        public abstract event Action<object> OnObjectReceived;
    }
}