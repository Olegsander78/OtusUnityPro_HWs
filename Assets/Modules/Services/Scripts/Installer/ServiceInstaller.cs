using UnityEngine;
using UnityEngine.Serialization;

namespace Services.Unity
{
    public sealed class ServiceInstaller : MonoBehaviour
    {
        [SerializeField]
        private bool installOnAwake;

        [Space]
        [SerializeField]
        private MonoBehaviour[] monoServices;

        [Space]
        [SerializeField]
        [FormerlySerializedAs("serviceLoaders")]
        private ServicePack[] servicePacks;

        private void Awake()
        {
            if (this.installOnAwake)
            {
                this.InstallServices();
            }
        }

        public void InstallServices()
        {
            this.InstallServicesFromBehaviours();
            this.InstallServicesFromPacks();
        }

        private void InstallServicesFromBehaviours()
        {
            for (int i = 0, count = this.monoServices.Length; i < count; i++)
            {
                var service = this.monoServices[i];
                ServiceLocator.AddService(service);
            }
        }

        private void InstallServicesFromPacks()
        {
            for (int i = 0, count = this.servicePacks.Length; i < count; i++)
            {
                var serviceLoader = this.servicePacks[i];
                var services = serviceLoader.LoadServices();
                for (int j = 0, length = services.Length; j < length; j++)
                {
                    var service = services[j];
                    ServiceLocator.AddService(service);
                }
            }
        }
    }
}