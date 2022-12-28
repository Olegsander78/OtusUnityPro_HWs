using Entities;


public interface IComponent_CanCollect
{
    bool CanCollect { get; }
}

public interface IComponent_Collect
{
    void Collect();
}

public interface IComponent_CollectEntity
{
    void Collect(IEntity entity);
}