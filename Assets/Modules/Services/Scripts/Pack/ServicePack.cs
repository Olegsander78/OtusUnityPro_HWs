using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if !UNITY_EDITOR
using System.Linq;
using System.Reflection;
#endif

// ReSharper disable NotAccessedField.Local

namespace Services
{
    [CreateAssetMenu(
        fileName = "ServicePack",
        menuName = "Service Locator/New ServicePack"
    )]
    public sealed class ServicePack : ServicePackBase
    {
#if UNITY_EDITOR
        [SerializeField]
        private bool editorMode;

        [SerializeField]
        private MonoScript[] releaseScripts;

        [SerializeField]
        private MonoScript[] editorScripts;
#endif
        [SerializeField]
        private string[] releaseClassNames;

        public override IEnumerable<object> ProvideServices()
        {
            object[] result;
#if UNITY_EDITOR
            if (this.editorMode)
            {
                result = LoadServicesInEditor(this.editorScripts);
            }
            else
            {
                result = LoadServicesInEditor(this.releaseScripts);
            }

#else
            result = LoadServicesInRelease();
#endif
            return result;
        }


#if UNITY_EDITOR
        private object[] LoadServicesInEditor(MonoScript[] scripts)
        {
            var count = scripts.Length;
            var result = new object[count];
            for (var i = 0; i < count; i++)
            {
                var script = scripts[i];
                if (script == null)
                {
                    Debug.LogWarning($"Missing script in service pack {this.name}");
                    continue;
                }
                
                var type = script.GetClass();
                var service = Activator.CreateInstance(type);
                if (service == null)
                {
                    Debug.LogWarning($"Service {type.Name} is null!");
                    continue;
                }

                result[i] = service;
            }

            return result;
        }
#else
        private object[] LoadServicesInRelease()
        {
            var count = this.releaseClassNames.Length;
            var result = new object[count];
            for (var i = 0; i < count; i++)
            {
                var className = this.releaseClassNames[i];
                var type = TypeResolver.GetType(className);
                if (type == null)
                {
                    throw new Exception($"Class {className} is not found!");
                }
                
                result[i] = Activator.CreateInstance(type);
            }

            return result;
        }
#endif

#if UNITY_EDITOR
        public void PrepareServicesForBuild()
        {
            var length = this.releaseScripts.Length;
            var classNames = new List<string>(length);
            for (var i = 0; i < length; i++)
            {
                var script = this.releaseScripts[i];
                if (script != null)
                {
                    var className = script.GetClass().FullName;
                    classNames.Add(className);
                }
            }

            this.releaseClassNames = classNames.ToArray();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}