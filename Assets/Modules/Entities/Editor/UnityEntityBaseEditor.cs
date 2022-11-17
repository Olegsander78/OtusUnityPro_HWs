#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Entities.UnityEditor
{
    [CustomEditor(typeof(UnityEntityBase))]
    public sealed class UnityEntityBaseEditor : Editor
    {
        private SerializedProperty elements;

        private UnityEntityBase entity;
        
        private DragAndDropDrawler dragAndDropDrawler;

        private void Awake()
        {
            this.elements = this.serializedObject.FindProperty(nameof(this.elements));
            this.entity = (UnityEntityBase) this.target;
        }

        private void OnEnable()
        {
            this.dragAndDropDrawler = new DragAndDropElementDrawler();
            this.dragAndDropDrawler.OnDragAndDrop += this.OnDragAndDrop;
        }
        
        private void OnDisable()
        {
            this.dragAndDropDrawler.OnDragAndDrop -= this.OnDragAndDrop;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            
            EditorGUILayout.Space(4.0f);
            EditorGUILayout.PropertyField(this.elements, includeChildren: true);
            
            EditorGUILayout.Space(8);
            this.dragAndDropDrawler.Draw();
            
            this.serializedObject.ApplyModifiedProperties();
        }
        
        private void OnDragAndDrop(Object draggedObject)
        {
            if (draggedObject is GameObject gameObject)
            {
                AddByGameObject(gameObject);
                EditorUtility.SetDirty(this.entity);
            }

            if (draggedObject is MonoBehaviour monoBehaviour)
            {
                this.AddByMonoBehaviour(monoBehaviour);
                EditorUtility.SetDirty(this.entity);
            }
        }

        private void AddByGameObject(GameObject gameObject)
        {
            var monoBehaviours = gameObject.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
            {
                this.AddByMonoBehaviour(monoBehaviour);
            }
        }

        private void AddByMonoBehaviour(MonoBehaviour monoBehaviour)
        {
            if (!ReferenceEquals(monoBehaviour, this.entity))
            {
                this.entity.Editor_AddElement(monoBehaviour);
            }
        }
    }
}
#endif