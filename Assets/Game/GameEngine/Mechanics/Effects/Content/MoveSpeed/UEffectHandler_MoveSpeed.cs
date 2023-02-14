using Elementary;
using UnityEngine;


public sealed class UEffectHandler_MoveSpeed : UEffectHandler
{
    [SerializeField]
    private MonoFloatVariable speedMultiplier;

    public override void OnEffectAdded(IEffect effect)
    {
        if (effect.TryGetParameter<float>(EffectParameterKey.MOVE_SPEED, out var multiplier))
        {
            this.speedMultiplier.Multiply(multiplier);
        }
    }

    public override void OnEffectRemoved(IEffect effect)
    {
        if (effect.TryGetParameter<float>(EffectParameterKey.MOVE_SPEED, out var multiplier))
        {
            this.speedMultiplier.Divide(multiplier);
        }
    }
}