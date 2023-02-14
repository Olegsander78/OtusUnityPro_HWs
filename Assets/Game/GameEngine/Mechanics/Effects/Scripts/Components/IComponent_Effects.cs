
public interface IComponent_GetEffect
{
    IEffect Effect { get; }
}

public interface IComponent_Effector
{
    void AddEffect(IEffect effect);

    void RemoveEffect(IEffect effect);
}