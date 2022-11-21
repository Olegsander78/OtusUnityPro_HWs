#if UNITY_EDITOR
using UnityEngine;

namespace GameElements.UnityEditor
{
    public sealed class DragAndDropServiceDrawler : DragAndDropDrawler
    {
        protected override Color ProvideColor()
        {
            ColorUtility.TryParseHtmlString("#60FFFF", out Color color);
            return color;
        }

        protected override string ProvideText()
        {
            return "Drag & Drop Game Services";
        }
    }
}
#endif