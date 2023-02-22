using System.Collections.Generic;

namespace PathFinding
{
    /// <summary>
    ///     <para>Finds a path by A* algorithm.</para> 
    /// </summary>
    public abstract class PathFinder<T> where T : class
    {
        private readonly Dictionary<T, Node> openList;

        private readonly HashSet<T> closedList;

        private T start;

        private T end;

        public PathFinder()
        {
            this.openList = new Dictionary<T, Node>();
            this.closedList = new HashSet<T>();
        }

        public bool FindPath(T start, T end, List<T> result)
        {
            result.Clear();
            
            if (ReferenceEquals(start, null) || ReferenceEquals(end, null))
            {
                return false;
            }
            
            if (ReferenceEquals(start, end))
            {
                return true;
            }
            
            this.start = start;
            this.end = end;

            this.openList.Clear();
            this.closedList.Clear();
            
            return this.FindPath(result);
        }

        protected virtual bool IsAvailable(T point)
        {
            return true;
        }

        protected abstract IEnumerable<T> GetNeighbours(T point);

        protected abstract float GetHeuristicDistance(T point1, T point2);

        protected virtual float GetCost(Node node)
        {
            return node.distanceToStart + node.distanceToEnd;
        }

        private bool FindPath(List<T> result)
        {
            var next = this.start;
            var nextNode = new Node(
                point: next,
                baseNode: null,
                distanceToStart: 0,
                distanceToEnd: this.GetHeuristicDistance(this.start, this.end)
            );

            while (true)
            {
                this.closedList.Add(next);
                this.ProcessNeighbours(nextNode);

                if (this.EndFound(out var endNode))
                {
                    this.CreatePath(endNode, result);
                    return true;
                }

                if (!this.SelectNext(out nextNode))
                {
                    return false;
                }

                next = nextNode.point;
                this.openList.Remove(next);
            }
        }

        private void ProcessNeighbours(Node node)
        {
            var neighbours = this.GetNeighbours(node.point);
            foreach (var point in neighbours)
            {
                this.ProcessNeighbour(point, node);
            }
        }

        private void ProcessNeighbour(T neighbour, Node baseNode)
        {
            if (this.closedList.Contains(neighbour))
            {
                return;
            }

            if (!this.IsAvailable(neighbour))
            {
                this.closedList.Add(neighbour);
                return;
            }

            var distance = this.GetHeuristicDistance(neighbour, baseNode.point);
            var distanceToStart = baseNode.distanceToStart + distance;
            var alreadyExists = this.openList.TryGetValue(neighbour, out var node);
            if (alreadyExists)
            {
                if (node.distanceToStart > distanceToStart)
                {
                    node.baseNode = baseNode;
                    node.distanceToStart = distanceToStart;
                }
            }
            else
            {
                var distanceToEnd = this.GetHeuristicDistance(neighbour, this.end);
                node = new Node(
                    point: neighbour,
                    baseNode: baseNode,
                    distanceToStart: distanceToStart,
                    distanceToEnd: distanceToEnd
                );

                this.openList.Add(neighbour, node);
            }
        }

        private bool EndFound(out Node node)
        {
            return this.openList.TryGetValue(this.end, out node);
        }

        private void CreatePath(Node endNode, List<T> result)
        {
            var currentNode = endNode;
            while (!ReferenceEquals(currentNode.point, this.start))
            {
                result.Add(currentNode.point);
                currentNode = currentNode.baseNode;
            }

            result.Add(this.start);
            result.Reverse();
        }

        private bool SelectNext(out Node result)
        {
            result = null;
            float resultWeight = -1;
            foreach (var nodeKV in this.openList)
            {
                var node = nodeKV.Value;
                var weight = this.GetCost(node);
                
                if (result == null || resultWeight > weight)
                {
                    result = node;
                    resultWeight = weight;
                }
            }

            return result != null;
        }

        protected sealed class Node
        {
            public readonly T point;

            public readonly float distanceToEnd;

            public Node baseNode;

            public float distanceToStart;

            public Node(T point, Node baseNode, float distanceToStart, float distanceToEnd)
            {
                this.point = point;
                this.baseNode = baseNode;
                this.distanceToStart = distanceToStart;
                this.distanceToEnd = distanceToEnd;
            }
        }
    }
}