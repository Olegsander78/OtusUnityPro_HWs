using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace AI.BTree.UnityEditor
{
    [CustomEditor(typeof(UnityBehaviourNode_Parallel))]
    public sealed class BehaviourNodeEditor_Parallel : Editor
    {
        private UnityBehaviourNode node;

        private void Awake()
        {
            this.node = (UnityBehaviourNode) this.target;
        }
        
        public override void OnInspectorGUI()
        {
            InspectorHelper.DrawRunningParameter(this.node.IsRunning);
            EditorGUILayout.Space(4.0f);
            
            GUI.enabled = false;
            GUILayout.Label("Children");

            var transform = this.node.transform;
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf && child.TryGetComponent(out UnityBehaviourNode node))
                {
                    EditorGUILayout.ObjectField(obj: node, objType: typeof(UnityBehaviourNode), allowSceneObjects: true);
                }
            }

            GUI.enabled = true;
        }
    }
}
#endif