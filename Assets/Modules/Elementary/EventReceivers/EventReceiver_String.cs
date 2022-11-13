using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Elementary
{
    public sealed class EventReceiver_String : MonoBehaviour
    {
        public event Action<string> OnEvent;

        [PropertySpace(8)]
        [GUIColor(0, 1, 0)]
        [Button]
        public void Call(string value)
        {
            this.OnEvent?.Invoke(value);
        }
    }
}