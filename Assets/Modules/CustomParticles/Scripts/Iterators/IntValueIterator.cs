using System;

namespace CustomParticles
{
    /// <summary>
    ///     <para>Moves from previous to next value by particle chunks.</para>
    /// </summary>
    public sealed class IntValueIterator : IValueIterator<int>
    {
        public int ParticleCount
        {
            get { return this.particleCount; }
        }

        public int CurrentValue
        {
            get { return this.currentValue; }
        }

        private readonly int particleCount;

        private readonly int valuePerParticle;

        private readonly int lastValuePerParticle;

        private int currentValue;

        private int pointer;

        public IntValueIterator(int previousValue, int newValue, int particleCount)
        {
            this.currentValue = previousValue;

            var diff = newValue - previousValue;
            if (diff == 0 || particleCount == 0)
            {
                return;
            }

            var valuePerParticle = diff / particleCount;
            if (Math.Abs(valuePerParticle) > 0)
            {
                this.valuePerParticle = valuePerParticle;
                this.lastValuePerParticle = this.valuePerParticle + diff % particleCount;
                this.particleCount = particleCount;
            }
            else
            {
                this.valuePerParticle = Math.Sign(diff);
                this.lastValuePerParticle = this.valuePerParticle;
                this.particleCount = Math.Abs(diff);
            }
        }

        public bool NextValue(out int value)
        {
            if (this.pointer > this.particleCount)
            {
                value = 0;
                return false;
            }

            this.pointer++;
            if (this.pointer < this.particleCount)
            {
                value = this.valuePerParticle;
                this.currentValue += value;
            }
            else
            {
                value = this.lastValuePerParticle;
                this.currentValue += value;
            }

            return true;
        }
    }
}