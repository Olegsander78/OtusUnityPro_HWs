using System;
using System.Collections.Generic;

namespace AI.Iterators
{
    public static class IteratorFactory
    {
        public static IEnumerator<T> CreateIterator<T>(IteratorMode mode, T[] points)
        {
            if (mode == IteratorMode.CIRCLE)
            {
                return new CircleIterator<T>(points);
            }

            if (mode == IteratorMode.FORWARD_BACK)
            {
                return new ForwardbackIterator<T>(points);
            }

            if (mode == IteratorMode.ROAD)
            {
                return new RoadIterator<T>(points);
            }

            throw new Exception($"Iterator {mode} is not found!");
        }
    }
}