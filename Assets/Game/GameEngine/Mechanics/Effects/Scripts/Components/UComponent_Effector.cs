using System;
using UnityEngine;
using UnityEngine.Serialization;


[AddComponentMenu("GameEngine/Mechanics/Effects/Component «Effector»")]
public sealed class UComponent_Effector : MonoBehaviour, IComponent_Effector
{
    public event Action<IEffect> OnEffectAdded
    {
        add { this.engine.OnEffectAdded += value; }
        remove { this.engine.OnEffectAdded -= value; }
    }

    public event Action<IEffect> OnEffectRemoved
    {
        add { this.engine.OnEffectRemoved += value; }
        remove { this.engine.OnEffectRemoved -= value; }
    }

    [FormerlySerializedAs("receiver")]
    [SerializeField]
    private UEffectEngine engine;

    public void AddEffect(IEffect effect)
    {
        this.engine.AddEffect(effect);
    }

    public void RemoveEffect(IEffect effect)
    {
        this.engine.RemoveEffect(effect);
    }
}