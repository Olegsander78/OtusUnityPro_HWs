using Elementary;
using System;
using UnityEngine;

[AddComponentMenu("GameEngine/Mechanics/Component «Melee Damage»")]
public class Component_MeleeDamage : MonoBehaviour,
        IComponent_GetMeleeDamage,
        IComponent_SetMeleeDamage,
        IComponent_OnMeleeDamageChanged
{
    public event Action<int> OnDamageChanged
    {
        add { _damage.OnValueChanged += value; }
        remove { _damage.OnValueChanged -= value; }
    }

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
}
