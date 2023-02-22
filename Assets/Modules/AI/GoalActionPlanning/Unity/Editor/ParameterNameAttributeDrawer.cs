#if UNITY_EDITOR
using System;
using AI.GOAP.Unity;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace AI.GOAP.UnityEditor
{
    public sealed class ParameterNameAttributeDrawer : OdinAttributeDrawer<StateNameAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var rect = EditorGUILayout.GetControlRect();
            GUIHelper.PushLabelWidth(GUIHelper.BetterLabelWidth);
            var nameRect = rect.AlignLeft(rect.width * 0.9f);

            var name = this.ValueEntry.SmartValue;
            var names = ParameterNamesConfig.GetNames();


            
            if (string.IsNullOrEmpty(name))
            {
                var currentIndex = 0;
                currentIndex = EditorGUI.Popup(nameRect, currentIndex, names);
                this.ValueEntry.SmartValue = names[currentIndex];
            }
            else if (Array.Exists(names, it => it == name))
            {
                var currentIndex = Array.IndexOf(names, name);
                currentIndex = EditorGUI.Popup(nameRect, currentIndex, names);
                this.ValueEntry.SmartValue = names[currentIndex];
            }
            else
            {
                this.ValueEntry.SmartValue = EditorGUI.TextField(nameRect, name);
            }

           

            // this.ValueEntry.SmartValue = SirenixEditorFields.Dropdown<string>(nameRect, label, name, names);

            GUIHelper.PopLabelWidth();
        }
    }

    // [CustomPropertyDrawer(typeof(FieldNameAttribute))]
    // public sealed class FieldNamePropertyDrawer : PropertyDrawer
    // {
    //     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //     {
    //         EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
    //
    //         var names = FieldsConfig.GetNames();
    //         var rect = EditorGUILayout.GetControlRect();
    //
    //         var selectedIndex = Array.IndexOf(names, property.stringValue);
    //         EditorGUI.Popup(rect, selectedIndex, names);
    //         property.stringValue = names[selectedIndex];
    //         property.serializedObject.ApplyModifiedProperties();
    //     }
    // }
}
#endif