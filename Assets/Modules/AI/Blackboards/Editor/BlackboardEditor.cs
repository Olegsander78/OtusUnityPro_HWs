#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AI.Blackboards.UnityEditor
{
    [CustomEditor(typeof(UnityBlackboard))]
    public sealed class BlackboardEditor : Editor
    {
        private UnityBlackboard blackboard;

        private void Awake()
        {
            this.blackboard = (UnityBlackboard) this.target;
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;

            var varibles = this.blackboard.Editor_GetVaribles();
            foreach (var variable in varibles)
            {
                this.DrawVariable(variable);
            }

            GUI.enabled = true;
        }

        private void DrawVariable(KeyValuePair<string, object> variable)
        {
            var name = variable.Key;
            var value = variable.Value;

            if (value is int intValue)
            {
                EditorGUILayout.IntField(name, intValue);
            }
            else if (value is float floatValue)
            {
                EditorGUILayout.FloatField(name, floatValue);
            }
            else if (value is string stringValue)
            {
                EditorGUILayout.TextField(name, stringValue);
            }
            else if (value is Object unityObject)
            {
                EditorGUILayout.ObjectField(name, obj: unityObject, objType: typeof(Object), allowSceneObjects: true);
            }
            else if (value is Vector2 vector2)
            {
                EditorGUILayout.Vector2Field(name, vector2);
            }
            else if (value is Vector3 vector3)
            {
                EditorGUILayout.Vector3Field(name, vector3);
            }
            else if (value is Enum enumValue)
            {
                EditorGUILayout.EnumPopup(enumValue);
            }
            else
            {
                if (value != null)
                {
                    EditorGUILayout.TextField(name, value.ToString());
                }
                else
                {
                    EditorGUILayout.TextField(name, "null");
                }
            }
        }
    }
}
#endif