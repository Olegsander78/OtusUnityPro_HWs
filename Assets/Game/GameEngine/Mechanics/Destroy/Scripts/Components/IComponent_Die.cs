using System;

public interface IComponent_Die 
{
    public event Action OnDieEvent;
    void Die();
}
