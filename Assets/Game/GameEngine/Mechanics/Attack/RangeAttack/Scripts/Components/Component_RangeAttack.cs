using UnityEngine;
using Elementary;

public class Component_RangeAttack : MonoBehaviour, IComponent_RangeAttack
{
    [SerializeField]
    private EventReceiver _attackReceiver;

    public void Attack()
    {
        _attackReceiver.Call();
    }
}
