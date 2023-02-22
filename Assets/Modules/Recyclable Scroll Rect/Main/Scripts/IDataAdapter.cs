
using UnityEngine;

namespace PolyAndCode.UI
{
    public interface IDataAdapter<T> where T : Component
    {
        int DataCount { get; }
        
        T ViewPrefab { get; }
        
        void OnAttachItem(T view, int index);
    }
}
