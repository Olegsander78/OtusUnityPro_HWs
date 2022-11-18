using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EventReceiver_EntityHW : MonoBehaviour
{
    public event Action<EntityHW> OnEvent;

    [Button]
    public void Call(EntityHW entity)
    {
        this.OnEvent?.Invoke(entity);
    }
}
