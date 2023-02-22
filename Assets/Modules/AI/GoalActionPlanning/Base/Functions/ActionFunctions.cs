namespace AI.GOAP
{
    public class ActionFunctions
    {
        public static bool FindCheapestAction(Parameter[] requiredParameters, IAction[] actions, out IAction result)
        {
            result = null;
            var currentCost = int.MaxValue;

            for (int i = 0, count = actions.Length; i < count; i++)
            {
                var action = actions[i];
                if (!action.SatisfiedState.Matches(requiredParameters))
                {
                    continue;
                }

                var cost = action.EvaluateCost();
                if (result == null || currentCost > cost)
                {
                    result = action;
                    currentCost = cost;
                }
            }

            return result != null;
        }
    }
}