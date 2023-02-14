
public interface IEffect
{
    T GetParameter<T>(EffectParameterKey name);

    bool TryGetParameter<T>(EffectParameterKey name, out T value);
}