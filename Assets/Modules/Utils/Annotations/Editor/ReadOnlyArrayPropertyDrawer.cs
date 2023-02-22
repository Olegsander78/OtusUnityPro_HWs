using Sirenix.OdinInspector.Editor;
using UnityEngine;

public sealed class ReadOnlyArrayPropertyDrawer : OdinAttributeDrawer<ReadOnlyArrayAttribute>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        GUI.enabled = false;
        this.CallNextDrawer(label);
        GUI.enabled = true;
    }
}