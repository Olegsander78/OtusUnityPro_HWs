#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    internal static class EditorValidator
    {
        internal static void ValidateElements(ref List<MonoBehaviour> gameElements)
        {
            var result = new HashSet<MonoBehaviour>();
            foreach (var gameElement in gameElements)
            {
                if (gameElement is IGameElement)
                {
                    result.Add(gameElement);
                }
            }

            gameElements = new List<MonoBehaviour>(result);
        }

        internal static void ValidateServices(ref List<MonoBehaviour> services)
        {
            var result = new HashSet<MonoBehaviour>();
            foreach (var service in services)
            {
                if (service != null)
                {
                    result.Add(service);
                }
            }

            services =  new List<MonoBehaviour>(result);
        }
    }
}
#endif