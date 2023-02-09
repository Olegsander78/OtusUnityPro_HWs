#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameSystem.UnityEditor
{
    public class DragAndDropDrawler
    {
        public event Action<Object> OnDragAndDrop;

        private readonly Color color;

        private readonly string text;
        
        public static DragAndDropDrawler CreateForServices(Action<Object> callback)
        {
            const string text = "Drag & Drop Game Services";
            ColorUtility.TryParseHtmlString("#60FFFF", out Color color);
            return new DragAndDropDrawler(text, color, callback);
        }

        public static DragAndDropDrawler CreateForElements(Action<Object> callback)
        {
            const string text = "Drag & Drop Game Elements";
            ColorUtility.TryParseHtmlString("#60FF60", out Color color);
            return new DragAndDropDrawler(text, color, callback);
        }

        public static DragAndDropDrawler CreateForConstructTasks(Action<Object> callback)
        {
            const string text = "Drag & Drop Construct Tasks";
            ColorUtility.TryParseHtmlString("#EDC232", out Color color);
            return new DragAndDropDrawler(text, color, callback);
        }

        private DragAndDropDrawler(string text, Color color, Action<Object> callback)
        {
            this.color = color;
            this.text = text;
            this.OnDragAndDrop += callback;
        }

        public void Draw()
        {
            var currentEvent = Event.current;
            var dropArea = GUILayoutUtility.GetRect(0.0f, 40.0f, GUILayout.ExpandWidth(true));
            var guiStyle = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleCenter
            };

            var prevColor = GUI.color;
            GUI.color = this.color;
            GUI.Box(dropArea, this.text, guiStyle);
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
    }
}
#endif