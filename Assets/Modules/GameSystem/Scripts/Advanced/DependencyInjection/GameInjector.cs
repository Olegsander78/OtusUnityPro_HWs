using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GameSystem
{
    ///FOR ADVANCED GAME ARCHITECTURE
    public static class GameInjector
    {
        private static readonly Type OBJECT_TYPE = typeof(object);

        private static readonly Type CONTEXT_TYPE = typeof(IGameContext);

        private static readonly Type MONO_BEHAVIOUR_TYPE = typeof(MonoBehaviour);

        private static readonly Type ATTRIBUTE_TYPE = typeof(GameInjectAttribute);

        public static T Instantiate<T>(IGameContext context)
        {
            return (T) Instantiate(context, typeof(T));
        }

        public static object Instantiate(IGameContext context, Type type)
        {
            var constructors = type.GetConstructors(System.Reflection.BindingFlags.Instance |
                                                    System.Reflection.BindingFlags.Public | 
                                                    System.Reflection.BindingFlags.DeclaredOnly);

            for (var i = 0; i < constructors.Length; i++)
            {
                var constructor = constructors[i];
                if (constructor.IsDefined(ATTRIBUTE_TYPE))
                {
                    return InstantiateByConstructor(context, constructor);
                }
            }
            
            var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
            if (defaultConstructor != null)
            {
                return defaultConstructor.Invoke(new object[0]);
            }

            throw new Exception("Constructor is not found!");
        }

        ///Use this methods to do dependency injection in our own class
        public static void InjectAll(IGameContext context, IEnumerable<object> targets)
        {
            foreach (var target in targets)
            {
                Inject(context, target);
            }
        }

        ///Use this methods to do dependency injection in our own class
        public static void Inject(IGameContext context, object target)
        {
            var type = target.GetType();

            while (true)
            {
                if (type == null || type == OBJECT_TYPE || type == MONO_BEHAVIOUR_TYPE)
                {
                    break;
                }

                InjectByFields(context, target, type);
                InjectByMethods(context, target, type);

                type = type.BaseType;
            }
        }

        public static void InjectByFields(IGameContext context, object target, Type targetType)
        {
            var fields = targetType.GetFields(System.Reflection.BindingFlags.Instance |
                                              System.Reflection.BindingFlags.Public |
                                              System.Reflection.BindingFlags.NonPublic |
                                              System.Reflection.BindingFlags.DeclaredOnly);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                var field = fields[i];
                if (field.IsDefined(ATTRIBUTE_TYPE))
                {
                    InjectByField(context, target, field);
                }
            }
        }

        public static void InjectByField(IGameContext context, object target, FieldInfo field)
        {
            var fieldType = field.FieldType;
            var value = ResolveReference(context, fieldType);
            field.SetValue(target, value);
        }

        public static void InjectByMethods(IGameContext context, object target, Type targetType)
        {
            var methods = targetType.GetMethods(System.Reflection.BindingFlags.Instance |
                                                System.Reflection.BindingFlags.Public |
                                                System.Reflection.BindingFlags.NonPublic |
                                                System.Reflection.BindingFlags.DeclaredOnly);

            for (int i = 0, count = methods.Length; i < count; i++)
            {
                var method = methods[i];
                if (method.IsDefined(ATTRIBUTE_TYPE))
                {
                    InjectByMethod(context, target, method);
                }
            }
        }

        public static void InjectByMethod(IGameContext context, object target, MethodInfo method)
        {
            var parameters = method.GetParameters();
            var count = parameters.Length;

            var args = new object[count];
            for (var i = 0; i < count; i++)
            {
                var parameter = parameters[i];
                args[i] = ResolveReference(context, parameter.ParameterType);
            }

            method.Invoke(target, args);
        }

        public static object ResolveReference(IGameContext context, Type type)
        {
            if (CONTEXT_TYPE.IsAssignableFrom(type))
            {
                return context;
            }

            if (type.IsArray)
            {
                return ResolveArrayReference(context, type);
            }

            if (!context.TryGetService(type, out var value))
            {
                LogWarning(type);
            }

            return value;
        }

        public static Array ResolveArrayReference(IGameContext context, Type arrayType)
        {
            var elementType = arrayType.GetElementType();
            var services = context.GetServices(elementType);
            var serviceCount = services.Length;

            var result = Array.CreateInstance(elementType!, serviceCount);
            Array.Copy(services, result, serviceCount);
            return result;
        }

        private static object InstantiateByConstructor(IGameContext context, ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            var count = parameters.Length;

            var args = new object[count];
            for (var i = 0; i < count; i++)
            {
                var parameter = parameters[i];
                args[i] = ResolveReference(context, parameter.ParameterType);
            }

            return constructor.Invoke(args);
        }

        private static void LogWarning(Type type)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Game Inejctor: Can't find value of type {type.Name}!");
#endif
        }
    }
}