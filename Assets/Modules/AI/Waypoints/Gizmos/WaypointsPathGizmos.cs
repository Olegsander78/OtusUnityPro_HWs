#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AI.Waypoints
{
    public static class WaypointsPathGizmos
    {
        public static void DrawRoads(List<Transform> points, bool loop, Color gizmosColor)
        {
            var previousColor = Handles.color;
            Handles.color = gizmosColor;

            var count = points.Count;
            if (count < 2)
            {
                return;
            }

            for (int i = 0, end = count - 1; i < end; i++)
            {
                var currentPoint = points[i];
                if (currentPoint == null)
                {
                    continue;
                }

                Transform nextPoint = null;
                for (var j = i + 1; j < count; j++)
                {
                    nextPoint = points[j];
                    if (nextPoint != null)
                    {
                        break;
                    }
                }

                if (nextPoint == null)
                {
                    break;
                }

                var startPosition = currentPoint.position;
                var endPosition = nextPoint.position;
                DrawLine(startPosition, endPosition);
            }

            var firstPosition = points[0].position;
            var lastPosition = points[^1].position;

            if (loop)
            {
                DrawLine(firstPosition, lastPosition);
            }
            else
            {
                NamePoint(firstPosition, "Start");
                NamePoint(lastPosition, "End");
            }
            
            Handles.color = previousColor;
        }

        private static void DrawLine(Vector3 startPosition, Vector3 endPosition)
        {
            try
            {
                Handles.DrawDottedLine(startPosition, endPosition, 4);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void NamePoint(Vector3 position, string name)
        {
            var style = new GUIStyle
            {
                fontSize = 12,
                alignment = TextAnchor.MiddleCenter,
                normal =
                {
                    textColor = Handles.color
                }
            };

            try
            {
                Handles.Label(position, name, style);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
#endif