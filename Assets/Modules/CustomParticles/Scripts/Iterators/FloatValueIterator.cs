using UnityEngine;

namespace CustomParticles
{
    /// <summary>
    ///     <para>Moves from previous to next value by particle chunks.</para>
    /// </summary>
    public sealed class FloatValueIterator : IValueIterator<float>
    {
        public int ParticleCount
        {
            get { return this.particleCount; }
        }

        public float CurrentValue
        {
            get { return this.currentValue; }
        }

        private readonly int particleCount;

        private readonly float valuePerParticle;

        private readonly float lastValuePerParticle;

        private float currentValue;

        private int pointer;

        public FloatValueIterator(float previousValue, float newValue, int particleCount)
        {
            this.currentValue = previousValue;

            var diff = newValue - previousValue;
            if (diff == 0 || particleCount == 0)
            {
                return;
            }

            var valuePerParticle = diff / particleCount;
            if (Mathf.Abs(valuePerParticle) > 0)
            {
                this.valuePerParticle = valuePerParticle;
                this.lastValuePerParticle = this.valuePerParticle + diff % particleCount;
                this.particleCount = particleCount;
            }
            else
            {
                this.valuePerParticle = Mathf.Sign(diff);
                this.lastValuePerParticle = this.valuePerParticle;
                this.particleCount = Mathf.RoundToInt(diff);
            }
        }

        public bool NextValue(out float value)
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