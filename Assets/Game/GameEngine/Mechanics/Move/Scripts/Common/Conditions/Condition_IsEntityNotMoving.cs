using Elementary;
using Entities;

namespace Game.GameEngine.Mechanics
{
    public sealed class Condition_IsEntityNotMoving : ICondition
    {
        private IComponent_IsMoving moveComponent;

        public Condition_IsEntityNotMoving(IEntity entity)
        {
            this.moveComponent = entity.Get<IComponent_IsMoving>();
        }

        public Condition_IsEntityNotMoving()
        {
        }

        public void SetEntity(IEntity entity)
        {
            this.moveComponent = entity.Get<IComponent_IsMoving>();
        }

        public bool IsTrue()
        {
            return !this.moveComponent.IsMoving;
        }
    }
}