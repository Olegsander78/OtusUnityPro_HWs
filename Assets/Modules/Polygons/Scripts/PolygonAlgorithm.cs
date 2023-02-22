using UnityEngine;

namespace Polygons
{
    public static class PolygonAlgorithm
    {
        public static float DistanceToLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            var projectPoint = ProjectToLine(point, lineStart, lineEnd);
            var distanceVector = projectPoint - point;
            return distanceVector.magnitude;
        }

        public static Vector2 ProjectToLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            var rhs = point - lineStart;
            var vector2 = lineEnd - lineStart;
            var magnitude = vector2.magnitude;
            var lhs = vector2;
            if (magnitude > 1E-06f)
            {
                lhs /= magnitude;
            }

            var num2 = Mathf.Clamp(Vector2.Dot(lhs, rhs), 0f, magnitude);
            return lineStart + lhs * num2;
        }

        public static bool ClampPosition(
            Vector2[] polygon,
            Vector2 point,
            out float targetDistance,
            out Vector2 targetPoint
        )
        {
            targetPoint = Vector2.zero;
            targetDistance = Mathf.Infinity;
            
            var pointCount = polygon.Length;
            if (pointCount < 3)
            {
                return false;
            }
            
            int i, j;
            
            for (i = 0, j = pointCount - 1; i < pointCount; j = i++)
            {
                var projectPoint = ProjectToLine(point, polygon[i], polygon[j]);
                var distanceVector = projectPoint - point;
                
                var currentDistance = distanceVector.magnitude;
                if (currentDistance < targetDistance)
                {
                    targetDistance = currentDistance;
                    targetPoint = projectPoint;
                }
            }

            return true;
        }

        public static float ClosestDistanceToPolygon(Vector2[] polygon, Vector2 point)
        {
            var nvert = polygon.Length;
            int i, j;
            var minDistance = Mathf.Infinity;

            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                var distance = DistanceToLine(point, polygon[i], polygon[j]);
                minDistance = Mathf.Min(minDistance, distance);
            }

            return minDistance;
        }


        public static bool IsInsidePolygon(Vector2[] polygon, Vector2 targetPoint)
        {
            var result = false;
            var count = polygon.Length;
            var j = count - 1;

            var targetY = targetPoint.y;
            var targetX = targetPoint.x;

            for (var i = 0; i < count; i++)
            {
                var currentPoint = polygon[i];
                var currentX = currentPoint.x;
                var currentY = currentPoint.y;

                var otherPoint = polygon[j];
                var otherX = otherPoint.x;
                var otherY = otherPoint.y;

                if (currentY < targetY && otherY >= targetY || otherY < targetY && currentY >= targetY)
                {
                    if (currentX + (targetY - currentY) / (otherY - currentY) * (otherX - currentX) < targetX)
                    {
                        result = !result;
                    }
                }

                j = i;
            }

            return result;
        }
    }
}