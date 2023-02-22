#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace AI.GOAP.UnityEditor
{
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;

    public sealed class ParameterPropertyDrawer : OdinValueDrawer<Parameter>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var rect = EditorGUILayout.GetControlRect();
            var field = this.ValueEntry.SmartValue;
            GUIHelper.PushLabelWidth(GUIHelper.BetterLabelWidth);

            var nameRect = rect.AlignLeft(rect.width * 0.9f);
            
            
            var name = field.name;
            var names = ParameterNamesConfig.GetNames();
            
            if (string.IsNullOrEmpty(name))
            {
                var currentIndex = 0;
                currentIndex = EditorGUI.Popup(nameRect, currentIndex, names);
                name = names[currentIndex];
            }
            else if (Array.Exists(names, it => it == name))
            {
                var currentIndex = Array.IndexOf(names, name);
                currentIndex = EditorGUI.Popup(nameRect, currentIndex, names);
                name = names[currentIndex];
            }
            else
            {
                name = EditorGUI.TextField(nameRect, name);
            }

            var previousColor = GUI.backgroundColor;
            var colorHTML = field.value ? "#B1EEF1" : "#FFDBBB";
            ColorUtility.TryParseHtmlString(colorHTML, out var color);
            GUI.backgroundColor = color;

            var valueRect = rect.AlignRight(rect.width * 0.05f);
            var value = EditorGUI.Toggle(valueRect, field.value);

            GUI.backgroundColor = previousColor;

            GUIHelper.PopLabelWidth();
            this.ValueEntry.SmartValue = new Parameter(name, value);
        }

#else
    // using System;
    //
    // [CustomPropertyDrawer(typeof(Field))]
    // public sealed class FieldPropertyDrawer : PropertyDrawer
    // {
    //     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //     {
    //         EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
    //
    //         var names = FieldsConfig.GetNames();
    //         var rect = EditorGUILayout.GetControlRect();
    //         var name = property.FindPropertyRelative("name");
    //         var value = property.FindPropertyRelative("value");
    //
    //         var selectedIndex = Array.IndexOf(names, name.stringValue);
    //         selectedIndex = EditorGUI.Popup(AlignLeft(rect, rect.width * 0.75f), selectedIndex, names);
    //         name.stringValue = names[selectedIndex];
    //         value.intValue = EditorGUI.IntField(AlignRight(rect, rect.width * 0.25f), value.intValue);
    //
    //         property.serializedObject.ApplyModifiedProperties();
    //     }
    //     
    //     private static Rect AlignLeft(Rect rect, float width)
    //     {
    //         rect.width = width;
    //         return rect;
    //     }
    //
    //     private static Rect AlignRight(Rect rect, float width)
    //     {
    //         rect.x = rect.x + rect.width - width;
    //         rect.width = width;
    //         return rect;
    //     }
#endif
    }
}
#endif