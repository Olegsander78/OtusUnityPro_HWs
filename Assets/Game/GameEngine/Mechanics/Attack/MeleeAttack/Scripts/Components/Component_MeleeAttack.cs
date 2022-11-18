using Elementary;
using Entities;
using UnityEngine;

public class Component_MeleeAttack : MonoBehaviour, IComponent_MeleeAttack
{
    [SerializeField]
    private EventReceiver_Entity _attackReceiver;

    public void Attack(UnityEntityBase target)
    {
        _attackReceiver.Call(target);
    }
}
