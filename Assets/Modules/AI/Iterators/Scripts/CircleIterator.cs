namespace AI.Iterators
{
    public sealed class CircleIterator<T> : Iterator<T>
    {
        public CircleIterator(T[] points) : base(points)
        {
        }

        public override bool MoveNext()
        {
            this.pointer = (this.pointer + 1) % this.movePoints.Length;
            return true;
        }

        public override void Reset()
        {
        }

        public override void Dispose()
        {
        }
    }
}