using UnityEngine;

namespace AI.GOAP.Unity
{
    public abstract class MonoStateController : MonoBehaviour, IWorldStateInject
    {
        [StateName]
        [SerializeField]
        protected string stateName;

        protected IWorldState worldState { get; private set; }

        IWorldState IWorldStateInject.WorldState
        {
            set { this.InjectWorldState(value); }
        }

        protected virtual void InjectWorldState(IWorldState worldState)
        {
            this.worldState = worldState;
        }
    }
}