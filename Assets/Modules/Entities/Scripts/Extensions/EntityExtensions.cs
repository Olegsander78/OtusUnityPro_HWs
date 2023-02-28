using System.Collections.Generic;

namespace Entities
{
    public static class EntityExtensions
    {
        public static void AddRange(this IEntity it, params object[] elements)
        {
            for (int i = 0, count = elements.Length; i < count; i++)
            {
                it.Add(elements[i]);
            }
        }

        public static void AddRange(this IEntity it, IEnumerable<object> elements)
        {
            foreach (var element in elements)
            {
                it.Add(element);
            }
        }
    }
}