using Elementary;
using UnityEngine;

public class Component_Die : MonoBehaviour, IComponent_Die
{
    [SerializeField]
    private EventReceiver _deathReceiver;
    public void Die()
    {
        _deathReceiver.Call();
    }
}
