namespace Services
{
    public abstract class AbstractFactory<T>
    {
        public T Instantiate()
        {
            var target = this.InstantiateObject();
            ServiceInjector.Inject(target);
            return target;
        }

        protected abstract T InstantiateObject();
    }
}