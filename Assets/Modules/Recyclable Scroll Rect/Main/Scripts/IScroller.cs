using UnityEngine;

namespace PolyAndCode.UI
{
    public interface IScroller
    {
        Vector2 DoScroll(Vector2 direction);
    }
}