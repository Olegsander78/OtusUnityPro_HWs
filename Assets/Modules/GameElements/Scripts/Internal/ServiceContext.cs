using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameElements
{
    internal sealed class ServiceContext
    {
        private readonly List<object> services;

        internal ServiceContext()
        {
            this.services = new List<object>();
        }

        internal T GetService<T>()
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                if (this.services[i] is T result)
                {
                    return result;
                }
            }
            
            throw new Exception($"Service {typeof(T).Name} is not found!");
        }

        internal object GetService(Type type)
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                var currentService = this.services[i];
                var currentType = currentService.GetType(); 
                
                if (type.IsAssignableFrom(currentType))
                {
                    return currentService;
                }
            }

            throw new Exception($"Service {type.Name} is not found!");
        }

        internal bool TryGetService(Type type, out object service)
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                var currentService = this.services[i];
                var currentType = currentService.GetType(); 
                
                if (type.IsAssignableFrom(currentType))
                {
                    service = currentService;
                    return true;
                }
            }

            service = default;
            return false;
        }
        
        internal bool TryGetService<T>(out T service)
        {
            for (int i = 0, count = this.services.Count; i < count; i++)
            {
                if (this.services[i] is T result)
                {
                    service = result;
                    return true;
                }
            }

            service = default;
            return false;
        }

        internal List<object> GetAllServices()
        {
            return this.services;
        }
        
        internal void AddService(object service)
        {
            if (service != null)
            {
                this.AddRecursively(service);
            }
        }

        internal void RemoveService(object service)
        {
            if (service != null)
            {
                this.RemoveRecursively(service);
            }
        }

        private void AddRecursively(object service)
        {
            if (service is IGameServiceGroup group)
            {
                var services = group.GetServices();
                foreach (var innerService in services)
                {
                    this.AddRecursively(innerService);
                }
            }
            else
            {
                this.services.Add(service);
            }
        }

        private void RemoveRecursively(object service)
        {
            if (service is IGameServiceGroup group)
            {
                var services = group.GetServices();
                foreach (var innerService in services)
                {
                    this.RemoveRecursively(innerService);
                }
            }
            else
            {
                this.services.Remove(service);
            }
        }
    }
}