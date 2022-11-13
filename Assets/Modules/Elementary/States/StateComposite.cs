using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/State «Composite»")]
    public class StateComposite : State
    {
        [SerializeField]
        private State[] stateComponents;

        public override void Enter()
        {
            for (int i = 0, count = this.stateComponents.Length; i < count; i++)
            {
                var state = this.stateComponents[i];
                state.Enter();
            }
        }

        public override void Exit()
        {
            for (int i = 0, count = this.stateComponents.Length; i < count; i++)
            {
                var state = this.stateComponents[i];
                state.Exit();
            }
        }
    }
}