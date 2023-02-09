using System;
using System.Collections.Generic;

namespace Services
{
    public interface IServiceContext
    {
        T GetService<T>();

        object GetService(Type serviceType);

        bool TryGetService<T>(out T service);

        bool TryGetService(Type serviceType, out object service);

        IEnumerable<T> GetServices<T>();

        IEnumerable<object> GetServices(Type serviceType);

        IEnumerable<object> GetAllServices();
        
        void AddService(object service);

        void AddServices(IEnumerable<object> services);

        void RemoveService(object service);
    }
}