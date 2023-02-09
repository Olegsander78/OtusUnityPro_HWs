using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    /// <summary>
    ///     <para>Global service registry</para>
    /// </summary>
    public static class ServiceLocator
    {
        private static IServiceContext instance;

        static ServiceLocator()
        {
            instance = new ServiceContext();
        }

        public static void SetContext(IServiceContext context)
        {
            instance = context;
        }

        public static T GetService<T>()
        {
            return instance.GetService<T>();
        }

        public static object GetService(Type serviceType)
        {
            return instance.GetService(serviceType);
        }

        public static IEnumerable<object> GetAllServices()
        {
            return instance.GetAllServices();
        }
        
        public static IEnumerable<T> GetServices<T>()
        {
            return instance.GetServices<T>();
        }

        public static IEnumerable<object> GetServices(Type serviceType)
        {
            return instance.GetServices(serviceType);
        }

        public static bool TryGetService(Type serviceType, out object service)
        {
            return instance.TryGetService(serviceType, out service);
        }
        
        public static bool TryGetService<T>(out T service)
        {
            return instance.TryGetService(out service);
        }

        public static void AddService(object service)
        {
            instance.AddService(service);
        }

        public static void AddServices(IEnumerable<object> services)
        {
            instance.AddServices(services);
        }

        public static void RemoveService(object service)
        {
            instance.RemoveService(service);
        }
    }
}