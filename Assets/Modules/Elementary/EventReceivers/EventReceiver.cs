using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Elementary
{
    public sealed class EventReceiver : MonoBehaviour
    {
        public event Action OnEvent;

        [PropertySpace(8)]
        [GUIColor(0, 1, 0)]
        [Button]
        public void Call()
        {
            this.OnEvent?.Invoke();
        }
    }
}