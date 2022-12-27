using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Event «Vector3»")]
    public sealed class EventBehaviour_Vector3 : MonoBehaviour
    {
        public event Action<Vector3> OnEvent;

        [Button]
        public void Call(Vector3 vector)
        {
            this.OnEvent?.Invoke(vector);
        }
    }
}