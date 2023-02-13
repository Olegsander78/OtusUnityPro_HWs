namespace Elementary
{
    public sealed class DecoratorValue<T> : IValue<T>
    {
        public T Value
        {
            get { return this.value.Value; }
        }

        private readonly IValue<T> value;

        public DecoratorValue(IValue<T> value)
        {
            this.value = value;
        }
    }
}