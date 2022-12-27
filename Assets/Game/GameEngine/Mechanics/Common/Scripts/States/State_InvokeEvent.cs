using Elementary;
using UnityEngine;
using UnityEngine.Events;

namespace Game.GameEngine.Mechanics
{
    public sealed class State_InvokeEvent : State
    {
        [SerializeField]
        private UnityEvent enterEvent;

        [SerializeField]
        private UnityEvent exitEvent;

        public override void Enter()
        {
            this.enterEvent.Invoke();
        }

        public override void Exit()
        {
            this.exitEvent.Invoke();
        }
    }
}