#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace AI.Blackboards.UnityEditor
{
    public sealed class BlackboardKeyAttributeDrawer : OdinAttributeDrawer<BlackboardKeyAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var names = BlackboardKeysConfig.EditorInstance.names;
            if (names.Length <= 0)
            {
                return;
            }

            GUIHelper.PushLabelWidth(GUIHelper.BetterLabelWidth);
            
            var name = this.ValueEntry.SmartValue;
            if (string.IsNullOrEmpty(name))
            {
                name = names[0];
            }

            var currentIndex = 0;
            if (Array.Exists(names, it => it == name))
            {
                currentIndex = Array.IndexOf(names, name);
            }
            
            currentIndex = EditorGUILayout.Popup(label, currentIndex, names);
            this.ValueEntry.SmartValue = names[currentIndex];

            GUIHelper.PopLabelWidth();
        }
    }
}
#endif