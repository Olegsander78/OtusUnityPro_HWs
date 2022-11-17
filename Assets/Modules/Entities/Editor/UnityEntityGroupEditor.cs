#if UNITY_EDITOR
using UnityEditor;

namespace Entities.UnityEditor
{
    [CustomEditor(typeof(UnityEntityGroup))]
    public sealed class UnityEntityGroupEditor : Editor
    {
        private SerializedProperty entities;
        
        private SerializedProperty elements;

        private void Awake()
        {
            this.entities = this.serializedObject.FindProperty(nameof(this.entities));
            this.elements = this.serializedObject.FindProperty(nameof(this.elements));
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            
            EditorGUILayout.Space(4.0f);
            EditorGUILayout.PropertyField(this.entities, includeChildren: true);

            EditorGUILayout.Space(4.0f);
            EditorGUILayout.PropertyField(this.elements, includeChildren: true);
            
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif