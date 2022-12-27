using UnityEngine;
using Elementary;
using System;

public class Component_RangeAttack : MonoBehaviour,
    IComponent_RangeAttack,
    IComponent_ProjectileRangeAttack
{
    public event Action<int> OnDamageChanged
    {
        add { _damage.OnValueChanged += value; }
        remove { _damage.OnValueChanged -= value; }
    }

    //[SerializeField]
    //private EventReceiver _attackReceiver;

    [SerializeField]
    private RangeAttackEngine _rangeAttackEngine;
    public int Damage
    {
        get { return _damage.Value; }
    }

    [SerializeField]
    private IntBehaviour _damage;    

    public void SetDamage(int damage)
    {
        _damage.Assign(damage);
    }
   

    public void Attack()
    {
        _rangeAttackEngine.TryShoot();
        //_attackReceiver.Call();
    }
}
