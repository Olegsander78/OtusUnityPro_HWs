using UnityEngine;

namespace AI.GOAP.Unity
{
    [AddComponentMenu("AI/GOAP/Action")]
    public sealed class BaseAction : AbstractAction
    {
        [Space]
        [SerializeField]
        private int cost = 1;

        public override int EvaluateCost()
        {
            return this.cost;
        }
    }
}