namespace AI.Iterators
{
    public sealed class RoadIterator<T> : Iterator<T>
    {
        public RoadIterator(T[] movePoints) : base(movePoints)
        {
        }

        public override bool MoveNext()
        {
            if (this.pointer + 1 < this.movePoints.Length)
            {
                this.pointer++;
                return true;
            }

            return false;
        }

        public override void Reset()
        {
            this.pointer = 0;
        }

        public override void Dispose()
        {
        }
    }
}