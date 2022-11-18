using UnityEngine;
using Elementary;

public class Component_RangeAttack : MonoBehaviour, IComponent_Attack
{
    [SerializeField]
    private EventReceiver_EntityHW _attackReceiver;

    public void Attack(EntityHW target)
    {
        _attackReceiver.Call(target);
    }
}
