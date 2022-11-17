#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Entities.UnityEditor
{
    [CustomEditor(typeof(UnityEntityProxy))]
    public sealed class UnityEntityProxyEditor : Editor
    {
        private SerializedProperty entity;

        private IEntity proxy;

        private void Awake()
        {
            this.entity = this.serializedObject.FindProperty(nameof(this.entity));
            this.proxy = (IEntity) this.target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(this.entity);
            this.serializedObject.ApplyModifiedProperties();

            GUI.enabled = false;

            try
            {
                this.DrawElements();
            }
            catch (Exception)
            {
            }
            
            GUI.enabled = true;
            
        }

        private void DrawElements()
        {
            EditorGUILayout.Space(4);
            var elements = this.proxy.GetAll<MonoBehaviour>();
            foreach (var element in elements)
            {
                EditorGUILayout.ObjectField(obj: element, objType: typeof(MonoBehaviour), allowSceneObjects: true);
            }
        }
    }
}
#endif