using UnityEngine;

namespace Elementary
{
    public interface ICollidersSensorListener
    {
        void OnCollidersUpdated(Collider[] buffer, int size);
    }
}