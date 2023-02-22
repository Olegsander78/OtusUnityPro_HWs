namespace AI.GOAP
{
    public interface IGoalPlanner
    {
        bool MakePlan(out Plan plan);
    }
}