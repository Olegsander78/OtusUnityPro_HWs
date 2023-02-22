using System.Collections.Generic;
using NUnit.Framework;
using PathFinding.Examples;

namespace PathFinding
{
    public sealed class PathfindingTests
    {
        [Test]
        public void WhenFindPathInSimpleGraph()
        {
            //Arrange:
            var point1 = new Point(-2, 3);
            var point2 = new Point(0, 0);
            var point3 = new Point(3, 2);

            var graph = new Dictionary<Point, IEnumerable<Point>>
            {
                {point1, new[] {point2}},
                {point2, new[] {point1, point3}},
                {point3, new[] {point2}}
            };

            var pathFinder = new PointPathFinder(graph);
            var pathResult = new List<Point>();

            //Act:
            var isPathFound = pathFinder.FindPath(point1, point3, pathResult);

            //Assert:
            Assert.True(isPathFound);

            Assert.AreEqual(point1, pathResult[0]);
            Assert.AreEqual(point2, pathResult[1]);
            Assert.AreEqual(point3, pathResult[2]);
        }

        [Test]
        public void WhenFindShortestPathInGraph()
        {
            //Arrange:
            var point1 = new Point(-2, 3);
            var point2 = new Point(0, 0);
            var point3 = new Point(2, 3);
            var point4 = new Point(3, 0);
            var point5 = new Point(6, 0);

            var graph = new Dictionary<Point, IEnumerable<Point>>
            {
                {point1, new[] {point2}},
                {point2, new[] {point1, point3, point4}},
                {point3, new[] {point2, point5}},
                {point4, new[] {point2, point5}},
                {point5, new[] {point3, point4}}
            };

            var pathFinder = new PointPathFinder(graph);
            var pathResult = new List<Point>();

            //Act:
            var isPathFound = pathFinder.FindPath(point1, point5, pathResult);

            //Assert:
            Assert.True(isPathFound);

            Assert.AreEqual(point1, pathResult[0]);
            Assert.AreEqual(point2, pathResult[1]);
            Assert.AreEqual(point4, pathResult[2]);
            Assert.AreEqual(point5, pathResult[3]);
        }

        [Test]
        public void WhenPathIsNotFound()
        {
            //Arrange:
            var point1 = new Point(-2, 3);
            var point2 = new Point(0, 0);
            var point3 = new Point(2, 3);
            var point4 = new Point(3, 0);
            var point5 = new Point(6, 0);

            var graph = new Dictionary<Point, IEnumerable<Point>>
            {
                {point1, new[] {point2}},
                {point2, new[] {point1, point3, point4}},
            };

            var pathFinder = new PointPathFinder(graph);
            var pathResult = new List<Point>();

            //Act:
            var isPathFound = pathFinder.FindPath(point1, point5, pathResult);

            //Assert:
            Assert.False(isPathFound);
        }

        [Test]
        public void WhenFindPathAndGraphIsEmpty()
        {
            //Arrange:
            var point1 = new Point(-2, 3);
            var point2 = new Point(0, 0);

            var pathFinder = new PointPathFinder(new Dictionary<Point, IEnumerable<Point>>());
            var pathResult = new List<Point>();

            //Act:
            var isPathFound = pathFinder.FindPath(point1, point2, pathResult);

            //Assert:
            Assert.False(isPathFound);
        }
    }
}