using Elementary;
using UnityEngine;
public class Component_MeleeDamage : MonoBehaviour,
        IComponent_GetMeleeDamage,
        IComponent_SetMeleeDamage
{
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
