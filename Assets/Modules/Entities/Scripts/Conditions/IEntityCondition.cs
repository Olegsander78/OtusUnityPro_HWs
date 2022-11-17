namespace Entities
{
    public interface IEntityCondition
    {
        bool IsTrue(IEntity entity);
    }
}