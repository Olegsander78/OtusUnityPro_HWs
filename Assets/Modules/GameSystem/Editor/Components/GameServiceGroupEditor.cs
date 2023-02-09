#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameSystem.UnityEditor
{
    [CustomEditor(typeof(MonoGameServiceGroup))]
    public sealed class GameServiceGroupEditor : Editor
    {
        private MonoGameServiceGroup serviceGroup;

        private SerializedProperty gameServices;

        private DragAndDropDrawler dragAndDropDrawler;

        private void OnEnable()
        {
            this.serviceGroup = (MonoGameServiceGroup) this.target;
            this.gameServices = this.serializedObject.FindProperty(nameof(this.gameServices));
            this.dragAndDropDrawler = DragAndDropDrawler.CreateForServices(this.OnDragAndDrop);
        }

        private void OnDisable()
        {
            this.dragAndDropDrawler.OnDragAndDrop -= this.OnDragAndDrop;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            this.DrawGameServices();
            this.serializedObject.ApplyModifiedProperties();
        }

        private void DrawGameServices()
        {
            EditorGUILayout.PropertyField(this.gameServices, includeChildren: true);
            EditorGUILayout.Space(8);
            this.dragAndDropDrawler.Draw();
        }
        
        private void OnDragAndDrop(Object draggedObject)
        {
            if (draggedObject is GameObject gameObject)
            {
                AddByGameObject(gameObject);
                EditorUtility.SetDirty(this.serviceGroup);
            }

            if (draggedObject is MonoBehaviour monoBehaviour)
            {
                this.AddByMonoBehaviour(monoBehaviour);
                EditorUtility.SetDirty(this.serviceGroup);
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
            if (ReferenceEquals(monoBehaviour, this.serviceGroup))
            {
                return;
            }
            
            if (monoBehaviour is IGameServiceGroup)
            {
                this.serviceGroup.Editor_AddService(monoBehaviour);
                return;
            }

            if (monoBehaviour is not IGameElementGroup)
            {
                this.serviceGroup.Editor_AddService(monoBehaviour);
            }
        }
    }
}
#endif