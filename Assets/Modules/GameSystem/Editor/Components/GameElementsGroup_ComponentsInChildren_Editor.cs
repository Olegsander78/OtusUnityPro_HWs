#if UNITY_EDITOR
using System.Collections.Generic;
using GameSystem.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameSystem.UnityEditor
{
    [CustomEditor(typeof(MonoGameElementGroup_ComponentsInChildren))]
    public sealed class GameElementsGroup_ComponentsInChildren_Editor : Editor
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
                EditorGUILayout.HelpBox("Game Element Group is inactive!", MessageType.Warning);
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
            var gameElements = new List<IGameElement>(capacity: 0);
            target.GetComponentsInChildren<IGameElement>(this.includeInactive.boolValue, gameElements);
            gameElements.Remove((IGameElement) target);

            foreach (var gameElement in gameElements)
            {
                var unityObject = (Object) gameElement;
                EditorGUILayout.ObjectField(obj: unityObject, objType: typeof(IGameElement), allowSceneObjects: true);
            }
        }
    }
}
#endif