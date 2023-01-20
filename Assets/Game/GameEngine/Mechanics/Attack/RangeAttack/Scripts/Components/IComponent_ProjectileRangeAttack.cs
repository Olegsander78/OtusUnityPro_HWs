using System;
public interface IComponent_ProjectileRangeAttack 
{
    event Action<int> OnDamageChanged;

    Projectile TryGetCurrentProjectile();

    int Damage { get; }
    void SetDamage(int damage);    
}
