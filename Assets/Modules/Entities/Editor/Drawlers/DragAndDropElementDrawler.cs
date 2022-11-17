#if UNITY_EDITOR
using UnityEngine;

namespace Entities.UnityEditor
{
    public sealed class DragAndDropElementDrawler : DragAndDropDrawler
    {
        protected override Color ProvideColor()
        {
            ColorUtility.TryParseHtmlString("#788EFF", out Color color);
            return color;
        }

        protected override string ProvideText()
        {
            return "Drag & Drop Elements";
        }
    }
}
#endif