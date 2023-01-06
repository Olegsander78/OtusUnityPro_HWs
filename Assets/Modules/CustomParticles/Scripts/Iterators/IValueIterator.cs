namespace CustomParticles
{
    /// <summary>
    ///     <para>Moves from previous to next value by particle chunks.</para>
    /// </summary>
    public interface IValueIterator<T>
    {
        public int ParticleCount { get; }

        public T CurrentValue { get; }
        
        public bool NextValue(out T value);
    }
}