using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PolyAndCode.UI
{
    public sealed class RecyclableScrollRect : ScrollRect
    {
        public int Segments
        {
            set { _segments = Math.Max(value, 2); }
            get { return _segments; }
        }

        [SerializeField]
        private bool IsGrid;

        [SerializeField]
        private DirectionType Direction;

        [SerializeField]
        private int _segments;

        private IScroller scroller;

        private Vector2 _prevAnchoredPos;

        protected override void Start()
        {
            this.vertical = Direction == DirectionType.Vertical;
            this.horizontal = Direction == DirectionType.Horizontal;
        }

        public void Initialize<T>(IDataAdapter<T> adapter) where T : Component
        {
            this._prevAnchoredPos = content.anchoredPosition;
            this.onValueChanged.RemoveListener(OnPositionChanged);
            this.StartCoroutine(this.InitializeRoutine(adapter));
        }

        private IEnumerator InitializeRoutine<T>(IDataAdapter<T> adapter) where T : Component
        {
            if (this.Direction == DirectionType.Vertical)
            {
                yield return this.InitializeAsVertical(adapter);
            }
            else if (this.Direction == DirectionType.Horizontal)
            {
                yield return this.InitializeAsHorizontal(adapter);
            }

            this.onValueChanged.AddListener(this.OnPositionChanged);
        }

        private IEnumerator InitializeAsVertical<T>(IDataAdapter<T> adapter) where T : Component
        {
            var scroller = new VerticalScroller<T>(this.viewport, this.content, adapter, this.IsGrid, this.Segments);
            yield return scroller.Start();
            this.scroller = scroller;
        }

        private IEnumerator InitializeAsHorizontal<T>(IDataAdapter<T> adapter) where T : Component
        {
            var scroller = new HorizontalScroller<T>(this.viewport, this.content, adapter, this.IsGrid, this.Segments);
            yield return scroller.Start();
            this.scroller = scroller;
        }

        private void OnPositionChanged(Vector2 normalizedPos)
        {
            var direction = this.content.anchoredPosition - this._prevAnchoredPos;
            this.m_ContentStartPosition += this.scroller.DoScroll(direction);
            this._prevAnchoredPos = this.content.anchoredPosition;
        }

        public enum DirectionType
        {
            Vertical,
            Horizontal
        }
    }
}