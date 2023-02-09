using UnityEngine;
using UnityEngine.Serialization;

namespace Services
{
    public sealed class ServiceInstaller : MonoBehaviour
    {
        [SerializeField]
        private bool installOnAwake;

        [Space, SerializeField]
        private MonoBehaviour[] monoServices;

        [Space, SerializeField, FormerlySerializedAs("serviceLoaders")]
        private ServicePackBase[] servicePacks;

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
            ServiceLocator.AddServices(this.monoServices);
        }

        private void InstallServicesFromPacks()
        {
            for (int i = 0, count = this.servicePacks.Length; i < count; i++)
            {
                var pack = this.servicePacks[i];
                var services = pack.ProvideServices();
                ServiceLocator.AddServices(services);
            }
        }
    }
}