using Elementary;
using UnityEngine;


public sealed class UEffectHandler_MeleeDamage : UEffectHandler
{
    [SerializeField]
    private MonoFloatVariable damageMultiplier;

    public override void OnEffectAdded(IEffect effect)
    {
        if (effect.TryGetParameter<float>(EffectParameterKey.DAMAGE, out var multiplier))
        {
            this.damageMultiplier.Multiply(multiplier);
        }
    }

    public override void OnEffectRemoved(IEffect effect)
    {
        if (effect.TryGetParameter<float>(EffectParameterKey.DAMAGE, out var multiplier))
        {
            this.damageMultiplier.Divide(multiplier);
        }
    }
}