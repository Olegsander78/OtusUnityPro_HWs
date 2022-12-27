using UnityEngine;
using Elementary;

public class Component_TakeDamageMechanics : MonoBehaviour, IComponent_TakeDamageMechanics
{
    [SerializeField]
    private EventReceiver_Int _takeDamageReceiver;
    public void TakeDamage(int damage)
    {
        _takeDamageReceiver.Call(damage);
    }
}
