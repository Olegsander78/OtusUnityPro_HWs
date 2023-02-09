#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using GameSystem;
using UnityEditor.Callbacks;

namespace GameSystem.UnityEditor
{
    public class GameElementsGizmos
    {
        private static readonly HashSet<string> libraryClassNames = new HashSet<string>
        {
            nameof(MonoGameContext),
            nameof(MonoGameElementGroup),
            nameof(MonoGameServiceGroup)
        };

        [DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            var annotation = Type.GetType("UnityEditor.Annotation, UnityEditor");
            var classIdField = annotation.GetField("classID");
            var scriptClassField = annotation.GetField("scriptClass");
            var flagsField = annotation.GetField("flags");
            var iconEnabledField = annotation.GetField("iconEnabled");

            var annotationUtilityField = Type.GetType("UnityEditor.AnnotationUtility, UnityEditor");
            var getAnnotationsMethod = annotationUtilityField.GetMethod("GetAnnotations",
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var setIconEnabledMethod = annotationUtilityField.GetMethod("SetIconEnabled",
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            var annotations = (Array) getAnnotationsMethod.Invoke(null, null);
            foreach (var a in annotations)
            {
                var classId = (int) classIdField.GetValue(a);
                var scriptClass = (string) scriptClassField.GetValue(a);
                var flags = (int) flagsField.GetValue(a);
                var iconEnabled = (int) iconEnabledField.GetValue(a);

                if (string.IsNullOrEmpty(scriptClass))
                {
                    continue;
                }

                const int HasIcon = 1;
                var hasIconFlag = (flags & HasIcon) == HasIcon;

                if (!libraryClassNames.Contains(scriptClass))
                {
                    continue;
                }

                if (hasIconFlag && iconEnabled != 0)
                {
                    setIconEnabledMethod.Invoke(null, new object[] {classId, scriptClass, 0});
                }
            }
        }
    }
}

#endif