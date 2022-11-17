using System;

namespace Entities
{
    public sealed class EntityConditon : IEntityCondition
    {
        private readonly Func<IEntity, bool> condition;
        
        public EntityConditon(Func<IEntity, bool> condition)
        {
            this.condition = condition;
        }
        
        public bool IsTrue(IEntity entity)
        {
            return this.condition.Invoke(entity);
        }
    }
}