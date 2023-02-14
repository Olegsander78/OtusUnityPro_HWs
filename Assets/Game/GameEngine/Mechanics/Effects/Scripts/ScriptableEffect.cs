using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ScriptableEffect : SerializedScriptableObject, IEffect
{
    [SerializeField]
    private IEffectParameter[] parameters = new IEffectParameter[0];

    private Effect effect;

    public T GetParameter<T>(EffectParameterKey name)
    {
        return this.effect.GetParameter<T>(name);
    }

    public bool TryGetParameter<T>(EffectParameterKey name, out T value)
    {
        return this.effect.TryGetParameter(name, out value);
    }

    protected override void OnAfterDeserialize()
    {
        base.OnAfterDeserialize();
        this.effect = new Effect(this.parameters);
    }
}