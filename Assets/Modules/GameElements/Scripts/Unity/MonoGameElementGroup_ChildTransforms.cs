using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace GameElements.Extensions
{
    [AddComponentMenu("GameElements/Game Element Group «Child Transforms»")]
    public sealed class MonoGameElementGroup_ChildTransforms : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        private bool includeInactive;
        
        public IEnumerable<IGameElement> GetElements()
        {
            if (!this.gameObject.activeSelf)
            {
                yield break;
            }

            if (this.includeInactive)
            {
                foreach (Transform child in this.transform)
                {
                    if (child.TryGetComponent(out IGameElement gameElement))
                    {
                        yield return gameElement;
                    }
                }
            }
            else
            {
                foreach (Transform child in this.transform)
                {
                    if (child.gameObject.activeSelf && 
                        child.TryGetComponent(out IGameElement gameElement))
                    {
                        yield return gameElement;
                    }
                }
            }
        }
    }
}