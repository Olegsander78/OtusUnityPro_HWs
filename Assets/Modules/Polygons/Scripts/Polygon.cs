using System.Linq;
using UnityEngine;

namespace Polygons
{
    public sealed class Polygon
    {
        public int Length
        {
            get { return this.points.Length; }
        }
        
        private readonly Vector2[] points;

        public Polygon(Vector2[] points)
        {
            var count = points.Length;
            this.points = new Vector2[count];
            for (var i = 0; i < count; i++)
            {
                var point = points[i];
                this.points[i] = point;
            }
        }

        public Vector2 GetPoint(int index)
        {
            return this.points[index];
        }

        public Vector2[] GetAllPoints()
        {
            return this.points.ToArray();
        }

        public bool IsPointInside(Vector2 point)
        {
            return PolygonAlgorithm.IsInsidePolygon(this.points, point);
        }

        public bool ClampPosition(Vector2 position, out float distance, out Vector2 clampedPosition)
        {
            return PolygonAlgorithm.ClampPosition(this.points, position, out distance, out clampedPosition);
        }
    }
}