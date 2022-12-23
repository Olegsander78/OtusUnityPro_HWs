using Elementary;
using Entities;

namespace Game.GameEngine.Mechanics
{
    public sealed class Condition_IsAliveEntity : ICondition
    {
        private IComponent_IsAlive component;

        public Condition_IsAliveEntity()
        {
        }

        public Condition_IsAliveEntity(IEntity target)
        {
            this.component = target.Get<IComponent_IsAlive>();
        }
        
        public void SetEntity(IEntity target)
        {
            this.component = target.Get<IComponent_IsAlive>();
        }

        public bool IsTrue()
        {
            return this.component.IsAlive;
        }
    }
}