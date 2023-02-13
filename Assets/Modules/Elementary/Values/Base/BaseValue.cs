namespace Elementary
{
    public sealed class BaseValue<T> : IValue<T>
    {
        public T Value { get; }

        public BaseValue(T value)
        {
            this.Value = value;
        }
    }
}