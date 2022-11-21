using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    [AddComponentMenu("GameElements/Game Element Group")]
    [DisallowMultipleComponent]
    public sealed class MonoGameElementGroup : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        private List<MonoBehaviour> gameElements = new List<MonoBehaviour>();

        public IEnumerable<IGameElement> GetElements()
        {
            for (int i = 0, count = this.gameElements.Count; i < count; i++)
            {
                yield return (IGameElement) this.gameElements[i];
            }
        }

#if UNITY_EDITOR
        public void Editor_AddElement(IGameElement element)
        {
            this.gameElements.Add((MonoBehaviour) element);
        }

        private void OnValidate()
        {
            EditorValidator.ValidateElements(ref this.gameElements);
        }
#endif
    }
}