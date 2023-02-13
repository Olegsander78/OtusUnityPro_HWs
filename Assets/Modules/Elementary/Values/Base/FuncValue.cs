using System;

namespace Elementary
{
    public sealed class FuncValue<T> : IValue<T>
    {
        public T Value
        {
            get { return this.function.Invoke(); }
        }

        private readonly Func<T> function;

        public FuncValue(Func<T> function)
        {
            this.function = function;
        }
    }
}