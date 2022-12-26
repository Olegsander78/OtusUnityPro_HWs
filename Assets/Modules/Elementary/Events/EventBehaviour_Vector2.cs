using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Event «Vector2»")]
    public sealed class EventBehaviour_Vector2 : MonoBehaviour
    {
        public event Action<Vector2> OnEvent;

        [Button]
        public void Call(Vector2 vector)
        {
            this.OnEvent?.Invoke(vector);
        }
    }
}