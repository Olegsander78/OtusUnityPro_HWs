#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameSystem.UnityEditor
{
    [CustomEditor(typeof(MonoGameElementGroup))]
    public sealed class GameElementGroupEditor : Editor
    {
        private MonoGameElementGroup elementGroup;

        private SerializedProperty gameElements;

        private DragAndDropDrawler dragAndDropDrawler;

        private void OnEnable()
        {
            this.elementGroup = (MonoGameElementGroup) this.target;
            this.gameElements = this.serializedObject.FindProperty(nameof(this.gameElements));
            this.dragAndDropDrawler = DragAndDropDrawler.CreateForElements(this.OnDragAndDrop);
        }

        private void OnDisable()
        {
            this.dragAndDropDrawler.OnDragAndDrop -= this.OnDragAndDrop;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            this.DrawGameElements();
            this.serializedObject.ApplyModifiedProperties();
        }

        private void DrawGameElements()
        {
            EditorGUILayout.PropertyField(this.gameElements, includeChildren: true);
            EditorGUILayout.Space(8);
            this.dragAndDropDrawler.Draw();
        }

        private void OnDragAndDrop(Object draggedObject)
        {
            if (draggedObject is GameObject gameObject)
            {
                this.AddByGameObject(gameObject);
                EditorUtility.SetDirty(this.elementGroup);
            }

            if (draggedObject is IGameElement gameElement)
            {
                this.AddByGameElement(gameElement);
                EditorUtility.SetDirty(this.elementGroup);
            }
        }

        private void AddByGameObject(GameObject gameObject)
        {
            var gameElements = gameObject.GetComponents<IGameElement>();
            foreach (var element in gameElements)
            {
                this.AddByGameElement(element);
            }
        }

        private void AddByGameElement(IGameElement element)
        {
            if (!ReferenceEquals(element, this.elementGroup))
            {
                this.elementGroup.Editor_AddElement(element);
            }
        }
    }
}
#endif