using System;
using Entities;
using Sirenix.OdinInspector;


public sealed class VisitingConveyorService
{
    [ReadOnly, ShowInInspector]
    public Zone InputZone { get; } = new();

    [ReadOnly, ShowInInspector]
    public Zone OutputZone { get; } = new();

    public sealed class Zone
    {
        public event Action<IEntity> OnEntered;

        public event Action<IEntity> OnExited;

        [ReadOnly]
        [ShowInInspector]
        public bool IsEntered { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        public IEntity TargetConveyor { get; private set; }

        public void Enter(IEntity conveyor)
        {
            if (this.IsEntered)
            {
                throw new Exception("Already entered into conveyor!");
            }

            this.TargetConveyor = conveyor;
            this.IsEntered = true;
            this.OnEntered?.Invoke(conveyor);
        }

        public void Exit()
        {
            if (!this.IsEntered)
            {
                return;
            }

            var previousConveyor = this.TargetConveyor;
            this.TargetConveyor = null;
            this.IsEntered = false;
            this.OnExited?.Invoke(previousConveyor);
        }
    }
}