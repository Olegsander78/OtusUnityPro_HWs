using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Event «Float»")]
    public sealed class EventBehaviour_Float : MonoBehaviour
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