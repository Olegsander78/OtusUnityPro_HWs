#if UNITY_EDITOR
using GameElements.Unity;
using UnityEditor;
using UnityEngine;

namespace GameElements.UnityEditor
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
            this.dragAndDropDrawler = new DragAndDropServiceDrawler();
            this.dragAndDropDrawler.OnDragAndDrop += this.OnDragAndDrop;
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
            if (monoBehaviour is IGameElementGroup || ReferenceEquals(monoBehaviour, this.serviceGroup))
            {
                return;
            }

            this.serviceGroup.Editor_AddService(monoBehaviour);
        }
    }
}
#endif