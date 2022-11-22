using Elementary;
using System;
using UnityEngine;

public class Component_Die : MonoBehaviour, IComponent_Die
{
    public event Action OnDieEvent;

    [SerializeField]
    private EventReceiver _deathReceiver;
    public void Die()
    {
        _deathReceiver.Call();

        OnDieEvent?.Invoke();
    }
}
