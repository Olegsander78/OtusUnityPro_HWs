#if UNITY_EDITOR
using UnityEditor;

namespace AI.Blackboards.UnityEditor
{
    internal static class BlackboardMenu
    {
        [MenuItem("Window/AI/Blackboard Keys", priority = 7)]
        internal static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(BlackboardKeysWindow));
        }

        [MenuItem("Tools/AI/Select Blackboard Keys Config", priority = 7)]
        internal static void SelectConfig()
        {
            Selection.activeObject = BlackboardKeysConfig.EditorInstance;
        }
    }
}
#endif