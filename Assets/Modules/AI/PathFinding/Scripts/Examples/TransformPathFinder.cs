using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathFinding.Examples
{
    public sealed class TransformPathFinder : PathFinder<Transform>
    {
        private readonly Dictionary<Transform, IEnumerable<Transform>> pointsTable;
    
        public TransformPathFinder(Dictionary<Transform, IEnumerable<Transform>> pointsTable)
        {
            this.pointsTable = pointsTable;
        }
        
        protected override IEnumerable<Transform> GetNeighbours(Transform point)
        {
            if (this.pointsTable.TryGetValue(point, out var points))
            {
                return points;
            }

            return Enumerable.Empty<Transform>();
        }

        protected override float GetHeuristicDistance(Transform point1, Transform point2)
        {
            return Vector3.Distance(point2.position, point1.position);
        }
    }
}