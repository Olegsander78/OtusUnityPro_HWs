using Elementary;
using Entities;


public sealed class Condition_CanCollectEntity : ICondition
{
    private IComponent_CanCollect component;

    public Condition_CanCollectEntity()
    {
    }

    public Condition_CanCollectEntity(IEntity entity)
    {
        this.component = entity.Get<IComponent_CanCollect>();
    }

    public void SetEntity(IEntity entity)
    {
        this.component = entity.Get<IComponent_CanCollect>();
    }

    public bool IsTrue()
    {
        return this.component.CanCollect;
    }
}