
using System;

public interface IComponent_ProjectileRangeAttack 
{
    event Action<int> OnDamageChanged;
    int Damage { get; }
    void SetDamage(int damage);    
}
