using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


[AddComponentMenu("GameEngine/Mechanics/Effects/Effect Engine")]
public sealed class UEffectEngine : MonoBehaviour
{
    public event Action<IEffect> OnEffectAdded;

    public event Action<IEffect> OnEffectRemoved;

    [ReadOnly]
    [ShowInInspector]
    private readonly List<IEffect> effects = new();

    [Space]
    [SerializeField]
    [FormerlySerializedAs("observers")]
    private List<UEffectHandler> handlers = new();

    public void AddEffect(IEffect effect)
    {
        for (var i = 0; i < this.handlers.Count; i++)
        {
            var handler = this.handlers[i];
            handler.OnEffectAdded(effect);
        }

        this.effects.Add(effect);
        this.OnEffectAdded?.Invoke(effect);
    }

    public void RemoveEffect(IEffect effect)
    {
        if (!this.effects.Remove(effect))
        {
            return;
        }

        for (var i = 0; i < this.handlers.Count; i++)
        {
            var handler = this.handlers[i];
            handler.OnEffectRemoved(effect);
        }

        this.OnEffectRemoved?.Invoke(effect);
    }

    public bool IsEffectExists(IEffect effect)
    {
        return this.effects.Contains(effect);
    }

    public IEffect[] GetEffects()
    {
        return this.effects.ToArray();
    }

    public void AddHandler(UEffectHandler handler)
    {
        this.handlers.Add(handler);
    }

    public void RemoveHandler(UEffectHandler handler)
    {
        this.handlers.Remove(handler);
    }
}