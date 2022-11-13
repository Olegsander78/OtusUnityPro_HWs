using System;
using UnityEngine;

namespace Game.Development
{
    public static class TileSnapper
    {
        public static void Snap(Transform targetTransform, int tileSize)
        {
            var position = targetTransform.position;
            var x = RoundValue(position.x, tileSize);
            var z = RoundValue(position.z, tileSize);
            targetTransform.position = new Vector3(x, 0.0f, z);
        }

        private static float RoundValue(float value, int size)
        {
            var roundedValue = Mathf.RoundToInt(value);
            if (roundedValue == 0)
            {
                return roundedValue;
            }

            var normalizedValue = roundedValue / size;
            var previousValue = normalizedValue * size;
            int nextValue;
            if (normalizedValue > 0)
            {
                nextValue = previousValue + size;
            }
            else
            {
                nextValue = previousValue - size;
            }

            if (Math.Abs(roundedValue - previousValue) < Math.Abs(roundedValue - nextValue))
            {
                return previousValue;
            }

            return nextValue;
        }
    }
}