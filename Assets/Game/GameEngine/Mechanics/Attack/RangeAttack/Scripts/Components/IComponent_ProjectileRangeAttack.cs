using System;
public interface IComponent_ProjectileRangeAttack 
{
    event Action<int> OnDamageChanged;
    Projectile CurrentProjectile { get; }
    int Damage { get; }
    void SetDamage(int damage);    
}
