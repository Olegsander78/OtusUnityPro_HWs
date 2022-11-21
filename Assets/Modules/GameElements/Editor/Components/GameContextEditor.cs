#if UNITY_EDITOR
using GameElements.Unity;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameElements.UnityEditor
{
    [CustomEditor(typeof(MonoGameContext))]
    public sealed class GameContextEditor : Editor
    {
        private MonoGameContext gameContext;

        private SerializedProperty autoRun;

        private SerializedProperty gameServices;

        private SerializedProperty gameElements;

        private DragAndDropDrawler dragAndDropServiceDrawler;

        private DragAndDropDrawler dragAndDropElementDrawler;

        private void OnEnable()
        {
            this.gameContext = (MonoGameContext) this.target;

            this.autoRun = this.serializedObject.FindProperty(nameof(this.autoRun));
            this.gameServices = this.serializedObject.FindProperty(nameof(this.gameServices));
            this.gameElements = this.serializedObject.FindProperty(nameof(this.gameElements));

            this.dragAndDropServiceDrawler = new DragAndDropServiceDrawler();
            this.dragAndDropServiceDrawler.OnDragAndDrop += this.OnDragAndDropService;

            this.dragAndDropElementDrawler = new DragAndDropElementDrawler();
            this.dragAndDropElementDrawler.OnDragAndDrop += this.OnDragAndDropElement;
        }

        private void OnDisable()
        {
            this.dragAndDropServiceDrawler.OnDragAndDrop -= this.OnDragAndDropService;
            this.dragAndDropElementDrawler.OnDragAndDrop -= this.OnDragAndDropElement;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(this.autoRun);
            EditorGUILayout.Space(2);

            GUI.enabled = false;
            EditorGUILayout.LabelField($"Status:  {this.gameContext.State}");
            GUI.enabled = true;

            EditorGUILayout.Space(2);
            this.DrawGameServices();
            EditorGUILayout.Space(10);
            this.DrawGameElements();

            this.serializedObject.ApplyModifiedProperties();
        }

        private void DrawGameServices()
        {
            EditorGUILayout.PropertyField(this.gameServices, includeChildren: true);
            EditorGUILayout.Space(8);
            this.dragAndDropServiceDrawler.Draw();
        }

        private void DrawGameElements()
        {
            EditorGUILayout.PropertyField(this.gameElements, includeChildren: true);
            EditorGUILayout.Space(8);
            this.dragAndDropElementDrawler.Draw();
        }

        private void OnDragAndDropElement(Object draggedObject)
        {
            if (draggedObject is GameObject gameObject)
            {
                this.AddElementByGameObject(gameObject);
                EditorUtility.SetDirty(this.gameContext);
            }

            if (draggedObject is IGameElement gameElement)
            {
                this.gameContext.Editor_AddElement((MonoBehaviour) gameElement);
                EditorUtility.SetDirty(this.gameContext);
            }
        }

        private void AddElementByGameObject(GameObject gameObject)
        {
            var gameElements = gameObject.GetComponents<IGameElement>();
            foreach (var element in gameElements)
            {
                this.gameContext.Editor_AddElement((MonoBehaviour) element);
            }
        }

        private void OnDragAndDropService(Object draggedObject)
        {
            if (draggedObject is GameObject gameObject)
            {
                this.AddServiceByGameObject(gameObject);
                EditorUtility.SetDirty(this.gameContext);
            }

            if (draggedObject is MonoBehaviour monoBehaviour)
            {
                this.AddServiceByMonoBehavour(monoBehaviour);
                EditorUtility.SetDirty(this.gameContext);
            }
        }

        private void AddServiceByMonoBehavour(MonoBehaviour monoBehaviour)
        {
            if (monoBehaviour is IGameElementGroup)
            {
                return;
            }

            this.gameContext.Editor_AddService(monoBehaviour);
        }

        private void AddServiceByGameObject(GameObject gameObject)
        {
            var monoBehaviours = gameObject.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
            {
                this.AddServiceByMonoBehavour(monoBehaviour);
            }
        }
    }
}
#endif