using UnityEngine;

public class Component_MeleeAttack : MonoBehaviour, IComponent_Attack
{
    [SerializeField]
    private EventReceiver_EntityHW _attackReceiver;

    public void Attack(EntityHW target)
    {
        _attackReceiver.Call(target);
    }
}
