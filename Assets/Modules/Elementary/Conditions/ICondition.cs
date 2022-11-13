namespace Elementary
{
    public interface ICondition
    {
        bool IsTrue();
    }

    public interface ICondition<in T>
    {
        bool IsTrue(T value);
    }
}