using UnityEngine;

namespace AI.GOAP.Unity
{
    [AddComponentMenu("AI/GOAP/Goal")]
    public sealed class BaseGoal : AbstractGoal
    {
        [Space]
        [SerializeField]
        private int priority;

        public override int EvaluatePriority()
        {
            return this.priority;
        }
    }
}