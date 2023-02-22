using System.Collections.Generic;

namespace AI.GOAP
{
    public static class PlanAlgorithm
    {
        public static bool BuildPlan(IGoal goal, IAction[] actions, IWorldState worldState, out Plan plan)
        {
            var targetParameters = goal.DesiredState;
            if (worldState.MatchesAllValues(targetParameters))
            {
                plan = new Plan(goal, new IAction[0]);
                return true;
            }

            var actionList = new List<IAction>();
            while (ActionFunctions.FindCheapestAction(targetParameters, actions, out var nextAction))
            {
                actionList.Add(nextAction);
                targetParameters = nextAction.RequiredState;

                if (worldState.MatchesAllValues(targetParameters))
                {
                    actionList.Reverse();
                    plan = new Plan(goal, actionList.ToArray());
                    return true;
                }
            }

            plan = default;
            return false;
        }
    }
}