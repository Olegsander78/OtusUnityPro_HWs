namespace AI.GOAP
{
    public static class WorldStateExtensions
    {
        public static bool ContainsAllNames(this IWorldState it, Parameter[] parameters)
        {
            for (int i = 0, count = parameters.Length; i < count; i++)
            {
                var state = parameters[i];
                if (!it.ContainsParameter(state.name))
                {
                    return false;
                }
            }

            return true;
        }
        
        public static bool MatchesAllValues(this IWorldState worldState, Parameter[] parameters)
        {
            for (int i = 0, count = parameters.Length; i < count; i++)
            {
                var field = parameters[i];
                if (!worldState.TryGetParameter(field.name, out var value))
                {
                    return false;
                }

                if (value != field.value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}