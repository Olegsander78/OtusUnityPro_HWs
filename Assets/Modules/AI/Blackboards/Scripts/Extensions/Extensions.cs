namespace AI.Blackboards
{
    public static class Extensions
    {
        public static void ReplaceVariable(this IBlackboard it, string key, object value)
        {
            it.RemoveVariable(key);
            it.AddVariable(key, value);
        }
    }
}