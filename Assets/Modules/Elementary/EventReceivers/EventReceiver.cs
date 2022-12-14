using System;
using Sirenix.OdinInspector;
using UnityEngine;

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
            Debug.Log($"Event was {name} received!");
            this.OnEvent?.Invoke();
        }
    }
}