#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace AI.Blackboards.UnityEditor
{
    public sealed class BlackboardKeysWindow : EditorWindow
    {
        private SerializedProperty names;

        private SerializedObject serializedObject;

        private Vector2 scrollPosition;

        private void Awake()
        {
            var config = BlackboardKeysConfig.EditorInstance;
            this.serializedObject = new SerializedObject(config);
            this.names = this.serializedObject.FindProperty(nameof(this.names));

            this.DrawTitle();
        }

        private void DrawTitle()
        {
            this.titleContent = new GUIContent("Blackboard Keys");
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(8);

            EditorGUILayout.BeginVertical();
            this.scrollPosition = EditorGUILayout.BeginScrollView(
                this.scrollPosition,
                GUILayout.ExpandWidth(true),
                GUILayout.ExpandHeight(true)
            );

            if (this.names != null)
            {
                EditorGUILayout.PropertyField(this.names, includeChildren: true);
            }


            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            if (this.serializedObject != null)
            {
                this.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
#endif