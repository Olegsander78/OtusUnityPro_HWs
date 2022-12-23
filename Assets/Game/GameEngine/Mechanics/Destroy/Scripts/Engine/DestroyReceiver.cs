using System;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class DestroyReceiver : MonoBehaviour
{
    public event Action<DestroyEvent> OnDestroy;

    [SerializeField]
    private DestroyAction[] actions;

    [Button]
    [GUIColor(0, 1, 0)]
    public void Invoke(DestroyEvent destroyEvent)
    {
        for (int i = 0, count = this.actions.Length; i < count; i++)
        {
            var action = this.actions[i];
            action.Do(destroyEvent);
        }

        this.OnDestroy?.Invoke(destroyEvent);
    }
}