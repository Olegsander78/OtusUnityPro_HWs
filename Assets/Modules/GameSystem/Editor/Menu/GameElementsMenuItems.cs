#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GameSystem.UnityEditor
{
    public static class GameElementsMenuItems
    {
        [MenuItem("GameObject/GameSystem/Game Context", false, 17)]
        public static void CreateGameSystem(MenuCommand menuCommand)
        {
            var root = new GameObject("GameContext").AddComponent<MonoGameContext>();
            Selection.activeGameObject = root.gameObject;

            var activeScene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(activeScene);
        }

        [MenuItem("GameObject/GameSystem/Game Element Group", false, 17)]
        public static void CreateGameElementGroup(MenuCommand menuCommand)
        {
            var root = new GameObject("GameElementGroup").AddComponent<MonoGameElementGroup>();
            Selection.activeGameObject = root.gameObject;

            var activeScene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(activeScene);
        }

        [MenuItem("GameObject/GameSystem/Game Service Group", false, 17)]
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