namespace AI.GOAP
{
    public static class ParameterExtensions
    {
        public static bool Matches(this Parameter[] state1, Parameter[] state2)
        {
            var count = state1.Length;
            if (count != state2.Length)
            {
                return false;
            }

            for (var i = 0; i < count; i++)
            {
                var field = state1[i];
                if (!state2.Contains(field))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Contains(this Parameter[] it, Parameter parameter)
        {
            var requiredName = parameter.name;
            var requiredValue = parameter.value;

            for (int i = 0, count = it.Length; i < count; i++)
            {
                var otherField = it[i];
                if (otherField.name == requiredName && otherField.value == requiredValue)
                {
                    return true;
                }
            }

            return false;
        }
    }
}