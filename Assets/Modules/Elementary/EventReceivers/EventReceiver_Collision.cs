using System;
using UnityEngine;

namespace Elementary
{
    [DisallowMultipleComponent]
    public sealed class EventReceiver_Collision : MonoBehaviour
    {
        public event Action<Collision> OnCollisionEntered;

        public event Action<Collision> OnCollisionStaying; 

        public event Action<Collision> OnCollisionExited;
        
        private void OnCollisionEnter(Collision collision)
        {
            this.OnCollisionEntered?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            this.OnCollisionStaying?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            this.OnCollisionExited?.Invoke(collision);
        }
    }
}