using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Elementary
{
    public sealed class EventReceiver_Float : MonoBehaviour
    {
        public event Action<float> OnEvent;

        [PropertySpace(8)]
        [GUIColor(0, 1, 0)]
        [Button]
        public void Call(float value)
        {
            this.OnEvent?.Invoke(value);
        }
    }
}