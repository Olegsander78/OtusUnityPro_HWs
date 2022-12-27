using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Event «Int»")]
    public sealed class EventBehaviour_Int : MonoBehaviour
    {
        public event Action<int> OnEvent;

        [PropertySpace(8)]
        [GUIColor(0, 1, 0)]
        [Button]
        public void Call(int value)
        {
            this.OnEvent?.Invoke(value);
        }
    }
}