namespace AI.GOAP
{
    public interface IAction
    {
        string Name { get; }
        
        Parameter[] RequiredState { get; }

        Parameter[] SatisfiedState { get; }

        int EvaluateCost();
    }
}