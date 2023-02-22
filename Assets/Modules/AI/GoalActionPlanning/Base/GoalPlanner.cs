using System.Collections.Generic;
using System.Linq;

namespace AI.GOAP
{
    public sealed class GoalPlanner : IGoalPlanner
    {
        private readonly IProvider provider;

        public GoalPlanner(IProvider provider)
        {
            this.provider = provider;
        }

        public bool MakePlan(out Plan plan)
        {
            var goals = this.provider
                .ProvideGoals()
                .OrderByDescending(it => it.EvaluatePriority())
                .ToArray();

            var actions = this.provider
                .ProvideActions()
                .ToArray();

            var worldState = this.provider
                .ProvideWorldState();

            for (int i = 0, count = goals.Length; i < count; i++)
            {
                var goal = goals[i];
                if (PlanAlgorithm.BuildPlan(goal, actions, worldState, out plan))
                {
                    return true;
                }
            }

            plan = default;
            return false;
        }

        public interface IProvider
        {
            IWorldState ProvideWorldState();

            IEnumerable<IGoal> ProvideGoals();

            IEnumerable<IAction> ProvideActions();
        }
    }
}