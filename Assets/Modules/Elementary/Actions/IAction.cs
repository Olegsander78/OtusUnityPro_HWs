namespace Elementary
{
    public interface IAction
    {
        void Do();
    }

    public interface IAction<in T>
    {
        void Do(T arg);
    }
}