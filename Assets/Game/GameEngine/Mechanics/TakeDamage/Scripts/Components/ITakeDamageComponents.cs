using System;


    public interface IComponent_TakeDamage
    {
        void TakeDamage(TakeDamageEvent damageEvent);
    }

public interface IComponent_OnDamageTaken
{
    event Action<TakeDamageEvent> OnDamageTaken;
}