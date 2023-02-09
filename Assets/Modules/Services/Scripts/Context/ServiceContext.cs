using System;
using System.Collections.Generic;

namespace Services
{
    public sealed class ServiceContext : IServiceContext
    {
        private readonly List<object> services;

        public ServiceContext()
        {
            this.services = new List<object>();
        }
        
        public ServiceContext(List<object> services)
        {
            this.services = new List<object>(services);
        }

        public object GetService(Type serviceType)
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                var currentService = this.services[i];
                var currentType = currentService.GetType(); 
                
                if (serviceType.IsAssignableFrom(currentType))
                {
                    return currentService;
                }
            }

            throw new Exception($"Service {serviceType.Name} is not found!");
        }

        public bool TryGetService<T>(out T result)
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                var service = this.services[i];
                if (service is T tService)
                {
                    result = tService;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public bool TryGetService(Type serviceType, out object service)
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                var currentService = this.services[i];
                var currentType = currentService.GetType(); 
                
                if (serviceType.IsAssignableFrom(currentType))
                {
                    service = currentService;
                    return true;
                }
            }

            service = default;
            return false;
        }

        public T GetService<T>()
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                var service = this.services[i];
                if (service is T result)
                {
                    return result;
                }
            }
            
            throw new Exception($"Service of type {typeof(T).Name} is not found!");
        }

        public IEnumerable<T> GetServices<T>()
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                var service = this.services[i];
                if (service is T tService)
                {
                    yield return tService;
                }
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                var service = this.services[i];
                var currentType = service.GetType(); 
                
                if (serviceType.IsAssignableFrom(currentType))
                {
                    yield return service;
                }
            }
        }

        public IEnumerable<object> GetAllServices()
        {
            return this.services;
        }

        public void AddServices(IEnumerable<object> services)
        {
            this.services.AddRange(services);
        }

        public void RemoveService(object service)
        {
            this.services.Remove(service);
        }

        public void AddService(object service)
        {
            this.services.Add(service);
        }
    }
}