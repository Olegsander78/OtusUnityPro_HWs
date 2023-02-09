#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Services.UnityEditor
{
    [CustomEditor(typeof(ServicePack))]
    public sealed class ServicePackEditor : Editor
    {
        private SerializedProperty editorMode;

        private SerializedProperty releaseScripts;

        private SerializedProperty editorScripts;

        private void OnEnable()
        {
            this.editorMode = this.serializedObject.FindProperty(nameof(this.editorMode));
            this.releaseScripts = this.serializedObject.FindProperty(nameof(this.releaseScripts));
            this.editorScripts = this.serializedObject.FindProperty(nameof(this.editorScripts));
        }

        public override void OnInspectorGUI()
        {
            if (this.editorMode.boolValue)
            {
                GUI.enabled = false;
            }

            EditorGUILayout.PropertyField(this.releaseScripts, includeChildren: true);
            GUI.enabled = true;

            EditorGUILayout.Space(4.0f);
            this.editorMode.boolValue = EditorGUILayout.BeginToggleGroup("Editor Mode", this.editorMode.boolValue);

            EditorGUILayout.Space(4.0f);
            EditorGUILayout.PropertyField(this.editorScripts, includeChildren: true);
            EditorGUILayout.EndToggleGroup();

            this.serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
#endif