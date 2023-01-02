using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    [AddComponentMenu("GameSystem/Game Service Group")]
    [DisallowMultipleComponent]
    public sealed class MonoGameServiceGroup : MonoBehaviour, IGameServiceGroup
    {
        [SerializeField]
        private List<MonoBehaviour> gameServices = new();

        public IEnumerable<object> GetServices()
        {
            for (int i = 0, count = this.gameServices.Count; i < count; i++)
            {
                yield return this.gameServices[i];
            }
        }

#if UNITY_EDITOR
        public void Editor_AddService(MonoBehaviour service)
        {
            this.gameServices.Add(service);
        }
        
        private void OnValidate()
        {
            EditorValidator.ValidateServices(ref this.gameServices);
        }
#endif
    }
}