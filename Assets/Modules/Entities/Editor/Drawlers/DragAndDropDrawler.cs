#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Entities.UnityEditor
{
    public abstract class DragAndDropDrawler
    {
        public event Action<Object> OnDragAndDrop;
        
        public void Draw()
        {
            var currentEvent = Event.current;
            var dropArea = GUILayoutUtility.GetRect(0.0f, 40.0f, GUILayout.ExpandWidth(true));
            var guiStyle = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleCenter
            };

            var prevColor = GUI.color;
            GUI.color = this.ProvideColor();
            GUI.Box(dropArea, this.ProvideText(), guiStyle);
            GUI.color = prevColor;

            if (currentEvent.type != EventType.DragUpdated && currentEvent.type != EventType.DragPerform)
            {
                return;
            }

            if (!dropArea.Contains(currentEvent.mousePosition))
            {
                return;
            }

            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            if (currentEvent.type != EventType.DragPerform)
            {
                return;
            }

            DragAndDrop.AcceptDrag();
            foreach (Object draggedObject in DragAndDrop.objectReferences)
            {
                this.OnDragAndDrop?.Invoke(draggedObject);
            }
        }

        protected abstract Color ProvideColor();

        protected abstract string ProvideText();
    }
}
#endif