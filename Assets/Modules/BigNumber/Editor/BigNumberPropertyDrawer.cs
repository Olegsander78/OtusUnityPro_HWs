#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public sealed class BigNumberPropertyDrawer : OdinValueDrawer<BigNumber>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var bigNumber = this.ValueEntry.SmartValue;
        var baseValue = bigNumber.GetBaseValue();
        Enum order = bigNumber.GetOrder();

        var rect = EditorGUILayout.GetControlRect();
        if (label != null)
        {
            rect = EditorGUI.PrefixLabel(rect, label);
        }

        GUIHelper.PushLabelWidth(50);
        baseValue = SirenixEditorFields.FloatField(rect.Split(0, 2), "Value", baseValue);
        order = SirenixEditorFields.EnumDropdown(rect.Split(2, 3), "Order", order);
        GUIHelper.PopLabelWidth();

        this.ValueEntry.SmartValue = new BigNumber(baseValue, (BigNumber.Order) order);
    }
}
#endif