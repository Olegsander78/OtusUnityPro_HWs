#if UNITY_EDITOR
using UnityEditor;

namespace AI.BTree.UnityEditor
{
    [CustomEditor(typeof(UnityBehaviourTree))]
    public sealed class BehaviourTreeEditor : Editor
    {
        private UnityBehaviourTree behaviourTree;

        private SerializedProperty root;

        private SerializedProperty updateMode;

        private SerializedProperty autoRun;

        private SerializedProperty loop;

        private void Awake()
        {
            this.behaviourTree = (UnityBehaviourTree) this.target;
            this.root = this.serializedObject.FindProperty(nameof(this.root));
            this.updateMode = this.serializedObject.FindProperty(nameof(this.updateMode));
            this.autoRun = this.serializedObject.FindProperty(nameof(this.autoRun));
            this.loop = this.serializedObject.FindProperty(nameof(this.loop));
        }

        public override void OnInspectorGUI()
        {
            if (this.behaviourTree.Editor_GetRoot() == null)
            {
                EditorGUILayout.HelpBox("Root is not installed", MessageType.Error, false);
            }
            else
            {
                InspectorHelper.DrawRunningParameter(this.behaviourTree.IsRunning);
            }

            EditorGUILayout.Space(2.0f);

            EditorGUILayout.PropertyField(this.autoRun);
            EditorGUILayout.PropertyField(this.loop);
            
            if (this.loop.boolValue)
            {
                EditorGUILayout.PropertyField(this.updateMode);
            }


            EditorGUILayout.Space(2.0f);
            EditorGUILayout.PropertyField(this.root, true);

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif