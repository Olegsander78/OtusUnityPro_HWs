using System.Collections.Generic;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/State «Composite»")]
    public class MonoStateComposite : MonoState
    {
        [SerializeField]
        private List<MonoState> stateComponents;

        public override void Enter()
        {
            for (int i = 0, count = this.stateComponents.Count; i < count; i++)
            {
                var state = this.stateComponents[i];
                state.Enter();
            }
        }

        public override void Exit()
        {
            for (int i = 0, count = this.stateComponents.Count; i < count; i++)
            {
                var state = this.stateComponents[i];
                state.Exit();
            }
        }

        public void AddState(MonoState state)
        {
            this.stateComponents.Add(state);
        }

        public void RemoveState(MonoState state)
        {
            this.stateComponents.Remove(state);
        }
    }
}