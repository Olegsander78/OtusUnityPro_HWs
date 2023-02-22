namespace AI.GOAP
{
    public readonly struct Plan
    {
        public readonly IGoal goal;

        public readonly IAction[] actions;

        public Plan(IGoal goal, IAction[] actions)
        {
            this.goal = goal;
            this.actions = actions;
        }

        public override string ToString()
        {
            return $"Goal: {this.goal}, Actions: {string.Join<IAction>(", ", this.actions)}";
        }
    }
}