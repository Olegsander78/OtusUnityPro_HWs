#if UNITY_EDITOR
using AI.Waypoints;
using UnityEditor;

namespace AI.UnityEditor
{
    [CustomEditor(typeof(WaypointsPath))]
    public sealed class WaypointsPathEditor : Editor
    {
        private SerializedProperty drawGizmos;

        private SerializedProperty loop;

        private SerializedProperty color;

        private void Awake()
        {
            this.drawGizmos = this.serializedObject.FindProperty(nameof(this.drawGizmos));
            this.loop = this.serializedObject.FindProperty(nameof(this.loop));
            this.color = this.serializedObject.FindProperty(nameof(this.color));
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(4.0f);
            
            this.drawGizmos.boolValue = EditorGUILayout.BeginToggleGroup("Draw Gizmos", this.drawGizmos.boolValue);
            EditorGUILayout.PropertyField(this.loop);
            EditorGUILayout.PropertyField(this.color);
            EditorGUILayout.EndToggleGroup();
            
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif