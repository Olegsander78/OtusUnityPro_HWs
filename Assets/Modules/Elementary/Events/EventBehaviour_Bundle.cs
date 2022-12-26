using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Event «Bundle»")]
    public sealed class EventBehaviour_Bundle : MonoBehaviour
    {
        public event Action<Bundle> OnEvent;

        [PropertySpace(8)]
        [GUIColor(0, 1, 0)]
        [Button]
        public void Call(Bundle bundle)
        {
            this.OnEvent?.Invoke(bundle);
        }
    }
}