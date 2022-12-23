using System;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Take Damage»")]
public sealed class Component_TakeDamage : MonoBehaviour,
    IComponent_TakeDamage,
    IComponent_OnDamageTaken
{
    public event Action<TakeDamageEvent> OnDamageTaken
    {
        add { this.engine.OnDamageTaken += value; }
        remove { this.engine.OnDamageTaken -= value; }
    }

    [SerializeField]
    private TakeDamageEngine engine;

    public void TakeDamage(TakeDamageEvent damageEvent)
    {
        this.engine.TakeDamage(damageEvent);
    }
}