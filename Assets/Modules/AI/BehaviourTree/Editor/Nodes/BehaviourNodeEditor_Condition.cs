#if UNITY_EDITOR
using UnityEditor;

namespace AI.BTree.UnityEditor
{
    [CustomEditor(typeof(UnityBehaviourNode_CheckCondition))]
    public sealed class BehaviourNodeEditor_Condition : Editor
    {
        private UnityBehaviourNode node;

        private SerializedProperty invertCondition;

        private SerializedProperty conditions;

        private void Awake()
        {
            this.node = (UnityBehaviourNode) this.target;
            this.invertCondition = this.serializedObject.FindProperty(nameof(this.invertCondition));
            this.conditions = this.serializedObject.FindProperty(nameof(this.conditions));
        }

        public override void OnInspectorGUI()
        {
            InspectorHelper.DrawRunningParameter(this.node.IsRunning);
            EditorGUILayout.Space(4.0f);
            
            this.invertCondition.boolValue = EditorGUILayout.Toggle("Invert Condition", this.invertCondition.boolValue);
            EditorGUILayout.PropertyField(this.conditions, true);

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif