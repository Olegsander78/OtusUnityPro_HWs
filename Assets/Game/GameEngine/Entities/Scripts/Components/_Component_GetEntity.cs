using Entities;


public sealed class _Component_GetEntity : IComponent_GetEntity
{
    public IEntity Entity { get; }

    public _Component_GetEntity(IEntity entity)
    {
        this.Entity = entity;
    }
}