using HenHen.Framework.Worlds.PathFinding;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Tests.Worlds.PathFinding
{
    public class PathFindingTests
    {
        public static IEnumerable<PathFindingTestCase> EnumerateTestCases()
        {
            var pointA = new PathPoint(new Vector2(-1, 0));
            var pointB = new PathPoint(new Vector2(1, 0));
            var pointC = new PathPoint(new Vector2(-2, 4));
            pointA.ConnectWith(pointB);
            pointB.ConnectWith(pointC);
            yield return new PathFindingTestCase(new PathRequest(pointA, pointB), 2);
            yield return new PathFindingTestCase(new PathRequest(pointA, pointC), 7);
        }

        [TestCaseSource(nameof(EnumerateTestCases)), Timeout(1000)]
        public void PathFindTest(PathFindingTestCase testCase)
        {
            var pathFinder = new PathFinder(testCase.PathRequest);
            while (!pathFinder.State.HasFlag(PathFindingState.Finished))
            {
                try
                {
                    pathFinder.Update();
                }
                catch (NotImplementedException)
                {
                    Assert.Ignore("Not yet implemented");
                }
            }
            Assert.AreEqual(testCase.Distance, PathLength(pathFinder.Result));
        }

        private static float PathLength(IReadOnlyList<PathPoint> pathPoints)
        {
            var length = 0f;
            for (var i = 0; i < pathPoints.Count - 1; i++)
                length += (pathPoints[i + 1].Position - pathPoints[i].Position).Length();
            return length;
        }

        public record PathFindingTestCase(PathRequest PathRequest, float Distance);
    }
}