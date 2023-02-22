#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace AI.BTree.UnityEditor
{
    internal static class InspectorHelper
    {
        internal static void DrawRunningParameter(bool isRunning)
        {
            GUI.enabled = false;
            var color = GUI.color;
            
            if (isRunning)
            {
                GUI.color = GetActiveColor();
            }
            
            EditorGUILayout.Toggle("Running", isRunning);
            
            GUI.color = color;
            GUI.enabled = true;
        }
        
        internal static Color GetActiveColor()
        {
            ColorUtility.TryParseHtmlString("#1D831D", out Color color);
            return color;
        }
    }
}
#endif