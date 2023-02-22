using UnityEngine;
using UnityEngine.Serialization;

namespace AI.GOAP.Unity
{
    public abstract class AbstractAction : MonoBehaviour, IAction, IWorldStateInject
    {
        public string Name
        {
            get { return this.actionName; }
        }

        public IWorldState WorldState { private get; set; }

        public virtual Parameter[] RequiredState
        {
            get { return this.requiredState; }
        }

        public virtual Parameter[] SatisfiedState
        {
            get { return this.satisfiedState; }
        }

        [SerializeField]
        private string actionName;

        [Space]
        [SerializeField]
        protected Parameter[] requiredState;

        [SerializeField]
        [FormerlySerializedAs("resultState")]
        protected Parameter[] satisfiedState;

        public abstract int EvaluateCost();

        public virtual bool IsValid()
        {
            if (this.WorldState == null)
            {
                return true;
            }

            if (!this.WorldState.ContainsAllNames(this.requiredState))
            {
                return false;
            }

            if (!this.WorldState.ContainsAllNames(this.satisfiedState))
            {
                return false;
            }

            return true;
        }

        protected virtual void Reset()
        {
            this.actionName = this.name;
        }

        public override string ToString()
        {
            return $"{this.name}";
        }
    }
}