#if UNITY_EDITOR
using GameElements.Unity;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GameElements.UnityEditor
{
    public static class GameElementsMenuItems
    {
        [MenuItem("GameObject/GameElements/Game System", false, 17)]
        public static void CreateGameSystem(MenuCommand menuCommand)
        {
            var root = new GameObject("GameSystem").AddComponent<MonoGameContext>();
            Selection.activeGameObject = root.gameObject;

            var activeScene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(activeScene);
        }

        [MenuItem("GameObject/GameElements/Game Element Group", false, 17)]
        public static void CreateGameElementGroup(MenuCommand menuCommand)
        {
            var root = new GameObject("GameElementGroup").AddComponent<MonoGameElementGroup>();
            Selection.activeGameObject = root.gameObject;

            var activeScene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(activeScene);
        }

        [MenuItem("GameObject/GameElements/Game Service Group", false, 17)]
        public static void CreateGameServiceGroup(MenuCommand menuCommand)
        {
            var root = new GameObject("GameServiceGroup").AddComponent<MonoGameServiceGroup>();
            Selection.activeGameObject = root.gameObject;

            var activeScene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(activeScene);
        }
    }
}
#endif