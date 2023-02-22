using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI.GOAP.Unity
{
    [AddComponentMenu("AI/GOAP/Goal Planner")]
    [DisallowMultipleComponent]
    public sealed class BaseGoalPlanner : MonoBehaviour, IGoalPlanner, GoalPlanner.IProvider
    {
        [SerializeField]
        private BaseWorldState worldState;

        [Space]
        [SerializeField]
        private AbstractGoal[] goals;

        [Space]
        [SerializeField]
        private AbstractAction[] actions;

        private readonly GoalPlanner planner;

        [ContextMenu("Make Plan")]
        public bool MakePlan(out Plan plan)
        {
            return this.planner.MakePlan(out plan);
        }

        IWorldState GoalPlanner.IProvider.ProvideWorldState()
        {
            return this.worldState;
        }

        IEnumerable<IGoal> GoalPlanner.IProvider.ProvideGoals()
        {
            return this.goals.Where(it => it.IsValid());
        }

        IEnumerable<IAction> GoalPlanner.IProvider.ProvideActions()
        {
            return this.actions.Where(it => it.IsValid());
        }

        public BaseGoalPlanner()
        {
            this.planner = new GoalPlanner(provider: this);
        }

#if UNITY_EDITOR
        public AbstractAction[] Editor_GetActions()
        {
            return this.actions;
        }

        public AbstractGoal[] Editor_GetGoals()
        {
            return this.goals;
        }
#endif
    }
}