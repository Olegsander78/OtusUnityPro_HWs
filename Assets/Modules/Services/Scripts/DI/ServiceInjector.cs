using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public static class ServiceInjector
    {
        private static readonly Type OBJECT_TYPE = typeof(object);

        private static readonly Type ATTRIBUTE_TYPE = typeof(ServiceInjectAttribute);
        
        public static T Instantiate<T>()
        {
            return (T) Instantiate(typeof(T));
        }

        public static object Instantiate(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Instance |
                                                    BindingFlags.Public | 
                                                    BindingFlags.DeclaredOnly);

            for (var i = 0; i < constructors.Length; i++)
            {
                var constructor = constructors[i];
                if (constructor.IsDefined(ATTRIBUTE_TYPE))
                {
                    return InstantiateInternal(constructor);
                }
            }

            var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
            if (defaultConstructor != null)
            {
                return defaultConstructor.Invoke(new object[0]);
            }

            throw new Exception("Constructor is not found!");
        }

        public static void ResolveDependencies()
        {
            var services = ServiceLocator.GetAllServices();
            InjectAll(services);
        }

        public static ConfiguredTaskAwaitable ResolveDependenciesAsync()
        {
            return Task
                .Run(ResolveDependencies)
                .ConfigureAwait(continueOnCapturedContext: true);
        }

        public static void InjectAll(IEnumerable<object> targets)
        {
            foreach (var target in targets)
            {
                Inject(target);
            }
        }

        public static void Inject(object target)
        {
            var type = target.GetType();

            while (true)
            {
                if (type == null || type == OBJECT_TYPE)
                {
                    break;
                }

                InjectByFields(target, type);
                InjectByMethods(target, type);

                type = type.BaseType;
            }
        }

        private static void InjectByFields(object target, Type targetType)
        {
            var fields = targetType.GetFields(BindingFlags.Instance |
                                              BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.DeclaredOnly);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                var field = fields[i];
                if (field.IsDefined(ATTRIBUTE_TYPE))
                {
                    InjectByField(target, field);
                }
            }
        }

        private static void InjectByField(object target, FieldInfo field)
        {
            var serviceType = field.FieldType;
            if (serviceType.IsArray)
            {
                var services = CreateArray(serviceType);
                field.SetValue(target, services);
            }
            else
            {
                if (!ServiceLocator.TryGetService(serviceType, out var service))
                {
                    LogWarning(serviceType);
                    return;
                }

                field.SetValue(target, service);
            }
        }

        private static void InjectByMethods(object target, Type targetType)
        {
            var methods = targetType.GetMethods(BindingFlags.Instance |
                                                BindingFlags.Public |
                                                BindingFlags.NonPublic |
                                                BindingFlags.DeclaredOnly);

            for (int i = 0, count = methods.Length; i < count; i++)
            {
                var method = methods[i];
                if (method.IsDefined(ATTRIBUTE_TYPE))
                {
                    InjectByMethod(target, method);
                }
            }
        }

        private static void InjectByMethod(object target, MethodInfo method)
        {
            var parameters = method.GetParameters();
            var count = parameters.Length;

            var args = new object[count];
            for (var i = 0; i < count; i++)
            {
                var parameter = parameters[i];
                var parameterType = parameter.ParameterType;
                object arg;

                if (parameterType.IsArray)
                {
                    arg = CreateArray(parameterType);
                }
                else
                {
                    if (!ServiceLocator.TryGetService(parameterType, out arg))
                    {
                        LogWarning(parameterType);
                    }
                }

                args[i] = arg;
            }

            method.Invoke(target, args);
        }

        private static Array CreateArray(Type arrayType)
        {
            var elementType = arrayType.GetElementType();
            var services = ServiceLocator.GetServices(elementType).ToArray();
            var serviceCount = services.Length;

            var result = Array.CreateInstance(elementType!, serviceCount);
            Array.Copy(services, result, serviceCount);
            return result;
        }
        
        private static object InstantiateInternal(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            var count = parameters.Length;

            var args = new object[count];
            for (var i = 0; i < count; i++)
            {
                var parameter = parameters[i];
                var parameterType = parameter.ParameterType;
                object arg;

                if (parameterType.IsArray)
                {
                    arg = CreateArray(parameterType);
                }
                else
                {
                    if (!ServiceLocator.TryGetService(parameterType, out arg))
                    {
                        LogWarning(parameterType);
                    }
                }

                args[i] = arg;
            }

            return constructor.Invoke(args);
        }

        private static void LogWarning(Type serviceType)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Can't inject missing service {serviceType.Name}!");
#endif
        }
    }
}