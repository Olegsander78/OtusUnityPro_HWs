using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// ReSharper disable InlineTemporaryVariable

namespace GameSystem
{
    public abstract class MonoGameInstaller : MonoBehaviour,
        IGameElementGroup,
        IGameServiceGroup,
        IGameConstructElement
    {
        private static readonly Type COMPONENT_ATTRIBUTE_TYPE = typeof(GameComponentAttribute);

        private static readonly Type INJECT_ATTRIBUTE_TYPE = typeof(GameInjectAttribute);

        private static readonly Type MONO_BEHAVIOUR_TYPE = typeof(MonoBehaviour);

        private static readonly Type OBJECT_TYPE = typeof(object);

        private Dictionary<Type, object> objectMap;

        public virtual IEnumerable<IGameElement> GetElements()
        {
            return this.GetElementsByReflection();
        }

        public virtual IEnumerable<object> GetServices()
        {
            return this.GetServicesByReflection();
        }

        public virtual void ConstructGame(IGameContext context)
        {
            this.ConstructByReflection(context);
        }

        protected IEnumerable<IGameElement> GetElementsByReflection()
        {
            var myType = this.GetType();
            var fields = myType.GetFields(BindingFlags.Instance |
                                          BindingFlags.Public |
                                          BindingFlags.NonPublic |
                                          BindingFlags.DeclaredOnly);
            for (int i = 0, count = fields.Length; i < count; i++)
            {
                var field = fields[i];
                if (!field.IsDefined(COMPONENT_ATTRIBUTE_TYPE))
                {
                    continue;
                }

                var componentAttribute = field.GetCustomAttribute<GameComponentAttribute>();
                if (componentAttribute.FlagsExists(BindingType.ELEMENT))
                {
                    yield return (IGameElement) field.GetValue(this);
                }
            }
        }

        private IEnumerable<object> GetServicesByReflection()
        {
            var myType = this.GetType();
            var fields = myType.GetFields(BindingFlags.Instance |
                                          BindingFlags.Public |
                                          BindingFlags.NonPublic |
                                          BindingFlags.DeclaredOnly);
            for (int i = 0, count = fields.Length; i < count; i++)
            {
                var field = fields[i];
                if (!field.IsDefined(COMPONENT_ATTRIBUTE_TYPE))
                {
                    continue;
                }

                var componentAttribute = field.GetCustomAttribute<GameComponentAttribute>();
                if (componentAttribute.FlagsExists(BindingType.SERVICE))
                {
                    yield return field.GetValue(this);
                }
            }
        }

        protected void ConstructByReflection(IGameContext context)
        {
            var myType = this.GetType();
            var fields = myType.GetFields(BindingFlags.Instance |
                                          BindingFlags.Public |
                                          BindingFlags.NonPublic |
                                          BindingFlags.DeclaredOnly);
            this.CreateObjectMap(fields);
            this.InjectAllObjects(context);
        }

        private void InjectAllObjects(IGameContext context)
        {
            var objectMap = new Dictionary<Type, object>(this.objectMap);
            foreach (var (targetType, targetValue) in objectMap)
            {
                this.InjectObject(context, targetType, targetValue);
            }
        }

        private void CreateObjectMap(FieldInfo[] fields)
        {
            this.objectMap = new Dictionary<Type, object>();
            for (int i = 0, count = fields.Length; i < count; i++)
            {
                var field = fields[i];
                if (!field.IsDefined(COMPONENT_ATTRIBUTE_TYPE))
                {
                    continue;
                }

                var fieldType = field.FieldType;
                if (this.objectMap.ContainsKey(fieldType))
                {
                    throw new Exception($"Field of type {field} is already exists! " +
                                        "Construct manually by method overriding!");
                }

                var value = field.GetValue(this);
                if (value == null)
                {
                    Debug.LogWarning($"Value of type {field.Name} is null!");
                }
                else
                {
                    this.objectMap.Add(fieldType, value);
                }
            }
        }


        private void InjectObject(IGameContext source, Type type, object target)
        {
            while (true)
            {
                if (type == null || type == OBJECT_TYPE || type == MONO_BEHAVIOUR_TYPE)
                {
                    break;
                }

                this.InjectByFields(source, target, type);
                this.InjectByMethods(source, target, type);

                type = type.BaseType;
            }
        }

        private void InjectByFields(IGameContext context, object target, Type targetType)
        {
            var fields = targetType.GetFields(BindingFlags.Instance |
                                              BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.DeclaredOnly);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                var field = fields[i];
                if (field.IsDefined(INJECT_ATTRIBUTE_TYPE))
                {
                    this.InjectByField(context, target, field);
                }
            }
        }

        private void InjectByField(IGameContext context, object target, FieldInfo field)
        {
         
            var fieldType = field.FieldType;
            if (!this.objectMap.TryGetValue(fieldType, out var value))
            {
                value = GameInjector.FindValue(context, fieldType);
            }

            field.SetValue(target, value);
        }

        private void InjectByMethods(IGameContext context, object target, Type targetType)
        {
            var methods = targetType.GetMethods(BindingFlags.Instance |
                                                BindingFlags.Public |
                                                BindingFlags.NonPublic |
                                                BindingFlags.DeclaredOnly);

            for (int i = 0, count = methods.Length; i < count; i++)
            {
                var method = methods[i];
                if (method.IsDefined(INJECT_ATTRIBUTE_TYPE))
                {
                    this.InjectByMethod(context, target, method);
                }
            }
        }

        private void InjectByMethod(IGameContext context, object target, MethodInfo method)
        {
            var parameters = method.GetParameters();
            var count = parameters.Length;

            var args = new object[count];
            for (var i = 0; i < count; i++)
            {
                var parameter = parameters[i];
                var parameterType = parameter.ParameterType;

                if (!this.objectMap.TryGetValue(parameterType, out var arg))
                {
                    arg = GameInjector.FindValue(context, parameterType);
                }

                args[i] = arg;
            }

            method.Invoke(target, args);
        }
    }
}