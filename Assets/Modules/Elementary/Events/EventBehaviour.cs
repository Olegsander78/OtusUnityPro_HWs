using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Event")]
    public sealed class EventBehaviour : MonoBehaviour
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