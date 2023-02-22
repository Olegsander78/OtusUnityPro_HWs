using System.Collections.Generic;
using UnityEngine;

namespace AI.Waypoints
{
    [AddComponentMenu("AI/Waypoints/Waypoints Path")]
    public sealed class WaypointsPath : MonoBehaviour
    {
        public List<Transform> GetTransformPoints()
        {
            var points = new List<Transform>();
            foreach (Transform point in this.transform)
            {
                if (point.gameObject.activeSelf)
                {
                    points.Add(point);
                }
            }

            return points;
        }

        public List<Vector3> GetPositionPoints()
        {
            var points = new List<Vector3>();
            foreach (Transform point in this.transform)
            {
                if (point.gameObject.activeSelf)
                {
                    points.Add(point.position);
                }
            }

            return points;
        }

#if UNITY_EDITOR

        [SerializeField]
        private bool drawGizmos;

        [SerializeField]
        private bool loop;

        [SerializeField]
        private Color color = Color.red;

        private void OnDrawGizmos()
        {
            if (this.drawGizmos)
            {
                var points = this.GetTransformPoints();
                WaypointsPathGizmos.DrawRoads(points, this.loop, this.color);
            }
        }
#endif
    }
}