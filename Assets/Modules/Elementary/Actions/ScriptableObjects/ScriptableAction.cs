namespace Elementary
{
    public abstract class ScriptableAction : IAction
    {
        public abstract void Do();
    }

    public abstract class ScriptableAction<T> : IAction<T>
    {
        public abstract void Do(T arg);
    }
}