namespace AI.GOAP
{
    public interface IGoal
    {
        string Name { get; }
        
        Parameter[] DesiredState { get; }

        int EvaluatePriority();
    }
}