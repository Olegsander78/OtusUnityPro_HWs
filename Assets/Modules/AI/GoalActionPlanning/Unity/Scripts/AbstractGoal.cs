using UnityEngine;
using UnityEngine.Serialization;

namespace AI.GOAP.Unity
{
    public abstract class AbstractGoal : MonoBehaviour, IGoal, IWorldStateInject
    {
        public IWorldState WorldState { protected get; set; }

        public string Name
        {
            get { return this.goalName; }
        }

        public virtual Parameter[] DesiredState
        {
            get { return this.desiredState; }
        }

        [SerializeField]
        private string goalName;

        [Space]
        [SerializeField]
        [FormerlySerializedAs("resultState")]
        protected Parameter[] desiredState;

        public abstract int EvaluatePriority();

        public virtual bool IsValid()
        {
            if (this.WorldState == null)
            {
                return true;
            }

            if (!this.WorldState.ContainsAllNames(this.desiredState))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{this.name}";
        }

        protected virtual void Reset()
        {
            this.goalName = this.name;
        }
    }
}