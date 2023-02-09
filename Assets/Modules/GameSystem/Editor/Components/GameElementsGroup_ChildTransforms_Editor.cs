#if UNITY_EDITOR
using GameSystem.Extensions;
using UnityEditor;
using UnityEngine;

namespace GameSystem.UnityEditor
{
    [CustomEditor(typeof(MonoGameElementGroup_ChildTransforms))]
    public sealed class GameElementsGroup_ChildTransforms_Editor : Editor
    {
        private SerializedProperty includeInactive;
        
        private bool showGameElements = true;
        
        private void Awake()
        {
            this.includeInactive = this.serializedObject.FindProperty(nameof(this.includeInactive));
        }

        public override void OnInspectorGUI()
        {
            var target = (MonoBehaviour) this.target;
            if (target.gameObject.activeSelf)
            {
                this.DrawIncludeField();
                this.DrawInfo(target);
            }
            else
            {
                EditorGUILayout.HelpBox("Game Elements Group is inactive!", MessageType.Warning);
            }
        }
        
        private void DrawIncludeField()
        {
            EditorGUILayout.Space(4.0f);
            EditorGUILayout.PropertyField(this.includeInactive);
            this.serializedObject.ApplyModifiedProperties();
        }

        private void DrawInfo(MonoBehaviour target)
        {
            EditorGUILayout.Space(4.0f);
            GUI.enabled = false;

            this.showGameElements = EditorGUILayout.Foldout(this.showGameElements, "Game Elements");
            if (this.showGameElements)
            {
                this.DrawGameElements(target);
            }
            
            GUI.enabled = true;
        }

        private void DrawGameElements(MonoBehaviour target)
        {
            var transform = target.transform;
            if (this.includeInactive.boolValue)
            {
                foreach (Transform child in transform)
                {
                    if (child.TryGetComponent(out IGameElement gameElement))
                    {
                        var unityObject = (Object) gameElement;
                        EditorGUILayout.ObjectField(obj: unityObject, objType: typeof(IGameElement), allowSceneObjects: true);
                    }
                }    
            }
            else
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.activeSelf && child.TryGetComponent(out IGameElement gameElement))
                    {
                        var unityObject = (Object) gameElement;
                        EditorGUILayout.ObjectField(obj: unityObject, objType: typeof(IGameElement), allowSceneObjects: true);
                    }
                }   
            }
        }
    }
}
#endif