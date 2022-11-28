using System;

public interface IComponent_GetMeleeDamage
{
    int Damage { get; }
}

public interface IComponent_SetMeleeDamage
{
    void SetDamage(int damage);
}
public interface IComponent_OnMeleeDamageChanged
{
    event Action<int> OnDamageChanged;
}
