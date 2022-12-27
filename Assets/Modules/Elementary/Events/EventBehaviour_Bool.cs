using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Event «Bool»")]
    public sealed class EventBehaviour_Bool : MonoBehaviour
    {
        public event Action<bool> OnEvent;

        [PropertySpace(8)]
        [GUIColor(0, 1, 0)]
        [Button]
        public void Call(bool value)
        {
            this.OnEvent?.Invoke(value);
        }
    }
}