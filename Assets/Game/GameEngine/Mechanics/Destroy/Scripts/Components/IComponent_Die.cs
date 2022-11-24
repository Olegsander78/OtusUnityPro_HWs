using System;

public interface IComponent_Die 
{
    public event Action OnDestroyedEvent;
    void Die();
}
