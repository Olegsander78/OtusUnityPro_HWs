#if UNITY_EDITOR
using UnityEngine;

namespace GameElements.UnityEditor
{
    public sealed class DragAndDropElementDrawler : DragAndDropDrawler
    {
        protected override Color ProvideColor()
        {
            ColorUtility.TryParseHtmlString("#60FF60", out Color color);
            return color;
        }

        protected override string ProvideText()
        {
            return "Drag & Drop Game Elements";
        }
    }
}
#endif