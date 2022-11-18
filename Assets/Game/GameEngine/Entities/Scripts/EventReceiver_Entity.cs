using System;
using Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    public class EventReceiver_Entity : MonoBehaviour
    {
        public event Action<UnityEntityBase> OnEvent;

        [Button]
        public void Call(UnityEntityBase entity)
        {
            OnEvent?.Invoke(entity);
        }
    }
}

