using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace GameElements.Extensions
{
    [AddComponentMenu("GameElements/Game Element Group «Components In Children»")]
    public sealed class MonoGameElementGroup_ComponentsInChildren : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        private bool includeInactive = true;
        
        public IEnumerable<IGameElement> GetElements()
        {
            var results = new List<IGameElement>(capacity: 0);
            if (this.gameObject.activeSelf)
            {
                this.GetComponentsInChildren<IGameElement>(this.includeInactive, results);
                results.Remove(this);
            }

            return results;
        }
    }
}