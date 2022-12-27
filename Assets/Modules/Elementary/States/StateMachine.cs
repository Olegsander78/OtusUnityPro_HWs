using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    public abstract class StateMachine<T> : State,
        ISerializationCallbackReceiver
        where T : Enum
    {
        public event Action<T> OnStateChanged;

        public T CurrentState
        {
            get { return this.mode; }
        }

        [Space]
        [SerializeField]
        private bool enterOnEnable;

        [SerializeField]
        private bool exitOnDisable;

        [OnValueChanged("SwitchState")]
        [Space]
        [SerializeField]
        private T mode;

        [SerializeField]
        private StateHolder[] states = Array.Empty<StateHolder>();

        private Dictionary<T, State> stateMap;

        private State currentState;

        private void OnEnable()
        {
            if (this.enterOnEnable)
            {
                this.Enter();
            }
        }

        private void OnDisable()
        {
            if (this.exitOnDisable)
            {
                this.Exit();
            }
        }
        
        public virtual void SwitchState(T state)
        {
            if (!ReferenceEquals(this.currentState, null))
            {
                this.currentState.Exit();
            }

            this.currentState = this.stateMap[state];
            this.currentState.Enter();
            this.mode = state;
            this.OnStateChanged?.Invoke(state);
        }

        [Title("Methods")]
        [GUIColor(0, 1, 0)]
        [Button]
        public override void Enter()
        {
            if (ReferenceEquals(this.currentState, null))
            {
                this.currentState = this.stateMap[this.mode];
                this.currentState.Enter();
            }
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public override void Exit()
        {
            if (!ReferenceEquals(this.currentState, null))
            {
                this.currentState.Exit();
                this.currentState = null;
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.stateMap = new Dictionary<T, State>();
            foreach (var info in this.states)
            {
                this.stateMap[info.mode] = info.state;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        [Serializable]
        private struct StateHolder
        {
            [SerializeField]
            public T mode;

            [SerializeField]
            public State state;
        }
    }
}