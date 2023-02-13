namespace Elementary
{
    public interface IValue<out T>
    {
        T Value { get; }
    }
}