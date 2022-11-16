using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    public sealed class EventReceiver_Vector3 : MonoBehaviour
    {
        public event Action<Vector3> OnEvent;

        [Button]
        public void Call(Vector3 vector)
        {
            this.OnEvent?.Invoke(vector);
        }
    }
}
