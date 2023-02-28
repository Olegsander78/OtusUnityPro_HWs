using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MonoOptimization
{
    public abstract class MonoModuleAuto : MonoModule
    {
        private readonly Type rootType;

        protected MonoModuleAuto()
        {
            this.rootType = this.GetType();
        }

        public override IEnumerable<IMonoComponent> ProvideMonoComponents()
        {
            return Provider.ProvideComponents(this.rootType, this);
        }

        public override void ConstructSensor(MonoContextModular context)
        {
            Constructor.ConstructModule(context, this.rootType, this);
        }

        private static class Provider
        {
            private static readonly Type MODULE_TYPE = typeof(MonoModuleAuto);

            private static readonly Type RESOLVE_ATTRIBUTE_TYPE = typeof(ResolveAttribute);

            private static readonly Type COMPONENT_ATTRIBUTE_TYPE = typeof(MonoComponentAttribute);

            internal static List<IMonoComponent> ProvideComponents(Type moduleType, object moduleValue)
            {
                var result = new List<IMonoComponent>();

                while (moduleType != MODULE_TYPE)
                {
                    ProvideComponentsRecurcively(moduleType, moduleValue, result);
                    moduleType = moduleType.BaseType;
                }

                return result;
            }

            private static void ProvideComponentsRecurcively(Type objectType, object objectValue,
                List<IMonoComponent> resultList)
            {
                var fields = objectType.GetFields(BindingFlags.Instance |
                                                  BindingFlags.Public |
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.DeclaredOnly);

                for (int i = 0, count = fields.Length; i < count; i++)
                {
                    var field = fields[i];
                    var fieldValue = field.GetValue(objectValue);
                    if (field.IsDefined(COMPONENT_ATTRIBUTE_TYPE) && fieldValue is IMonoComponent component)
                    {
                        resultList.Add(component);
                    }

                    if (field.IsDefined(RESOLVE_ATTRIBUTE_TYPE))
                    {
                        ProvideComponentsRecurcively(field.FieldType, fieldValue, resultList);
                    }
                }
            }
        }

        private static class Constructor
        {
            private static readonly Type MODULE_TYPE = typeof(MonoModuleAuto);

            private static readonly Type GAME_OBJECT_TYPE = typeof(GameObject);

            private static readonly Type CONSTRUCT_ATTRIBUTE_TYPE = typeof(ResolveAttribute);

            internal static void ConstructModule(MonoContextModular context, Type moduleType, object moduleValue)
            {
                while (moduleType != MODULE_TYPE)
                {
                    ConstructNodeRecurcively(
                        context: context,
                        moduleType: moduleType,
                        moduleValue: moduleValue,
                        nodeType: moduleType,
                        nodeValue: moduleValue
                    );
                    moduleType = moduleType.BaseType;
                }
            }

            private static void ConstructNodeRecurcively(
                MonoContextModular context,
                Type moduleType,
                object moduleValue,
                Type nodeType,
                object nodeValue
            )
            {
                var fields = nodeType.GetFields(BindingFlags.Instance |
                                                BindingFlags.Public |
                                                BindingFlags.NonPublic |
                                                BindingFlags.DeclaredOnly);

                for (int i = 0, count = fields.Length; i < count; i++)
                {
                    var field = fields[i];
                    var fieldValue = field.GetValue(nodeValue);
                    if (field.IsDefined(CONSTRUCT_ATTRIBUTE_TYPE))
                    {
                        ConstructNodeRecurcively(context, moduleType, moduleValue, field.FieldType, fieldValue);
                    }
                }

                var methods = nodeType.GetMethods(BindingFlags.Instance |
                                                  BindingFlags.Public |
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.DeclaredOnly);

                for (int i = 0, count = methods.Length; i < count; i++)
                {
                    var method = methods[i];
                    if (method.IsDefined(CONSTRUCT_ATTRIBUTE_TYPE))
                    {
                        var args = ResolveArgs(context, moduleType, moduleValue, method);
                        method.Invoke(nodeValue, args);
                    }
                }
            }

            private static object[] ResolveArgs(
                MonoContextModular context,
                Type moduleType,
                object moduleValue,
                MethodInfo method
            )
            {
                var parameters = method.GetParameters();
                var count = parameters.Length;

                var args = new object[count];
                for (var i = 0; i < count; i++)
                {
                    var parameter = parameters[i];
                    var parameterType = parameter.ParameterType;
                    args[i] = ResolveArg(context, parameterType, moduleType, moduleValue);
                }

                return args;
            }

            private static object ResolveArg(
                MonoContextModular context,
                Type parameterType,
                Type moduleType,
                object moduleValue
            )
            {
                if (parameterType == typeof(MonoContextModular))
                {
                    return context;
                }
                
                if (parameterType == GAME_OBJECT_TYPE)
                {
                    return context.gameObject;
                }

                if (parameterType == typeof(MonoBehaviour))
                {
                    return context;
                }

                if (context.TryGetModule(parameterType, out var module))
                {
                    return module;
                }

                var type = moduleType;
                while (type != MODULE_TYPE)
                {
                    if (ResolveArgRecurcively(
                            parameterType: parameterType,
                            parameterValue: out var parameterValue,
                            nodeType: type,
                            nodeValue: moduleValue,
                            hasChildrenNodes: true
                        ))
                    {
                        return parameterValue;
                    }

                    type = type.BaseType;
                }

                Debug.LogWarning($"Can't resolve arg {parameterType.Name}");
                return null;
            }

            private static bool ResolveArgRecurcively(
                Type parameterType,
                out object parameterValue,
                Type nodeType,
                object nodeValue,
                bool hasChildrenNodes
            )
            {
                if (parameterType == nodeType)
                {
                    parameterValue = nodeValue;
                    return true;
                }

                if (!hasChildrenNodes)
                {
                    parameterValue = default;
                    return false;
                }

                var fields = nodeType.GetFields(BindingFlags.Instance |
                                                BindingFlags.Public |
                                                BindingFlags.NonPublic |
                                                BindingFlags.DeclaredOnly);
                for (int i = 0, count = fields.Length; i < count; i++)
                {
                    var field = fields[i];
                    var fieldType = field.FieldType;
                    var fieldValue = field.GetValue(nodeValue);
                    var hasChildren = field.IsDefined(CONSTRUCT_ATTRIBUTE_TYPE);
                    if (ResolveArgRecurcively(parameterType, out parameterValue, fieldType, fieldValue, hasChildren))
                    {
                        return true;
                    }
                }

                parameterValue = default;
                return false;
            }
        }
    }
}