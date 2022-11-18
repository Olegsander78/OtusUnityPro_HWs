using UnityEngine;
using Elementary;

public class Component_TakeDamage : MonoBehaviour, IComponent_TakeDamage
{
    [SerializeField]
    private EventReceiver_Int _takeDamageReceiver;
    public void TakeDamage(int damage)
    {
        _takeDamageReceiver.Call(damage);
    }
}
