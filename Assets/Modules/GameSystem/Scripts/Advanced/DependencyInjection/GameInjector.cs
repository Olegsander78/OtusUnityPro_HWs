using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// ReSharper disable InlineTemporaryVariable

namespace GameSystem
{
    ///FOR ADVANCED GAME ARCHITECTURE
    public static class GameInjector
    {
        private static readonly Type OBJECT_TYPE = typeof(object);

        private static readonly Type CONTEXT_TYPE = typeof(IGameContext);

        private static readonly Type MONO_BEHAVIOUR_TYPE = typeof(MonoBehaviour);

        private static readonly Type ATTRIBUTE_TYPE = typeof(GameInjectAttribute);

        ///Use this methods to do dependency injection in our own class
        public static void InjectAll(IGameContext source, IEnumerable<object> targets)
        {
            foreach (var target in targets)
            {
                Inject(source, target);
            }
        }

        ///Use this methods to do dependency injection in our own class
        public static void Inject(IGameContext source, object target)
        {
            var type = target.GetType();

            while (true)
            {
                if (type == null || type == OBJECT_TYPE || type == MONO_BEHAVIOUR_TYPE)
                {
                    break;
                }

                InjectByFields(source, target, type);
                InjectByMethods(source, target, type);

                type = type.BaseType;
            }
        }

        public static void InjectByFields(IGameContext context, object target, Type targetType)
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
                    InjectByField(context, target, field);
                }
            }
        }

        public static void InjectByField(IGameContext context, object target, FieldInfo field)
        {
            var fieldType = field.FieldType;
            var value = FindValue(context, fieldType);
            field.SetValue(target, value);
        }

        public static void InjectByMethods(IGameContext context, object target, Type targetType)
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
                args[i] = FindValue(context, parameter.ParameterType);
            }

            method.Invoke(target, args);
        }

        public static object FindValue(IGameContext context, Type type)
        {
            if (CONTEXT_TYPE.IsAssignableFrom(type))
            {
                return context;
            }

            if (type.IsArray)
            {
                return FindValueAsArray(context, type);
            }

            if (!context.TryGetService(type, out var value))
            {
                LogWarning(type);
            }

            return value;
        }

        public static Array FindValueAsArray(IGameContext context, Type arrayType)
        {
            var elementType = arrayType.GetElementType();
            var services = context.GetServices(elementType);
            var serviceCount = services.Length;

            var result = Array.CreateInstance(elementType!, serviceCount);
            Array.Copy(services, result, serviceCount);
            return result;
        }

        private static void LogWarning(Type type)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Game Inejctor: Can't find value of type {type.Name}!");
#endif
        }
    }
}