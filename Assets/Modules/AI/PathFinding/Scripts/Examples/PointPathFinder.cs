using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathFinding.Examples
{
    public sealed class Point
    {
        public readonly float x;
        
        public readonly float y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public sealed class PointPathFinder : PathFinder<Point>
    {
        private readonly Dictionary<Point, IEnumerable<Point>> pointsTable;

        public PointPathFinder(Dictionary<Point, IEnumerable<Point>> pointsTable)
        {
            this.pointsTable = pointsTable;
        }

        protected override IEnumerable<Point> GetNeighbours(Point point)
        {
            if (this.pointsTable.TryGetValue(point, out var points))
            {
                return points;
            }

            return Enumerable.Empty<Point>();
        }

        protected override float GetHeuristicDistance(Point point1, Point point2)
        {
            var dx = point1.x - point2.x;
            var dy = point1.y - point2.y;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }
    }
}