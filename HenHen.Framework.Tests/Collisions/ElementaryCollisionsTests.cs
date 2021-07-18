// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Collisions;
using HenHen.Framework.Numerics;
using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Tests.Collisions
{
    public class ElementaryCollisionsTests
    {
        private static IEnumerable<(Vector2 point, Circle circle, bool expected)> PointCircleTestCases => new[]
        {
            (new Vector2(), new Circle { CenterPosition = new Vector2(), Radius = 1 }, true),
            (new Vector2(), new Circle { CenterPosition = new Vector2(5), Radius = 1 }, false),
            (new Vector2(0, 4), new Circle { CenterPosition = new Vector2(0, 5), Radius = 1 }, true),
            (new Vector2(0, 4), new Circle { CenterPosition = new Vector2(1, 5), Radius = 1 }, false),
            (new Vector2(1), new Circle { Radius = 1.41421356237f /* sqrt(2) */ }, true)
        };

        private static IEnumerable<(Vector2 point, Triangle2 triangle, bool expected)> PointTriangleTestCases => new[]
        {
            (new Vector2(),
            new Triangle2(new Vector2(-1, -1), new Vector2(0, 1), new Vector2(1, -1) ),
            true),
            (new Vector2(),
            new Triangle2(new Vector2(-2, 1), new Vector2(2, 1), new Vector2(2, 0) ),
            false),
            (new Vector2(1, 1),
            new Triangle2(new Vector2(1, 1), new Vector2(0, 0), new Vector2(0.5f, 0) ),
            true),
            (new Vector2(1, 1),
            new Triangle2(new Vector2(2, 2), new Vector2(), new Vector2(2, 0)),
            true),
            (new Vector2(1, 1.0001f),
            new Triangle2(new Vector2(2, 2), new Vector2(), new Vector2(2, 0)),
            false),
            (new Vector2(1.00001f, 0.999f),
            new Triangle2(new Vector2(2, 2), new Vector2(), new Vector2(2, 0) ),
            true),
            (new Vector2(0.999f, 0.9999f),
            new Triangle2(new Vector2(2, 2), new Vector2(), new Vector2(2, 0) ),
            false)
        };

        private static IEnumerable<(Vector2 point, RectangleF rect, bool expected)> PointRectTestCases => new[]
        {
            (new Vector2(), new RectangleF(-1, 1, -1, 1), true),
            (new Vector2(2), new RectangleF(-1, 1, 1, -1), false),
            (new Vector2(1), new RectangleF(-1, 1, -1, 1), true),
        };

        private static IEnumerable<(Circle a, Circle b, bool expected)> CirclesTestCases => new[]
        {
            (new Circle{ Radius = 1 }, new Circle { Radius = 1 }, true),
            (new Circle{ CenterPosition = new Vector2(1, 0), Radius = 1 }, new Circle { Radius = 1 }, true),
            (new Circle{ CenterPosition = new Vector2(1, 0), Radius = 1 }, new Circle { CenterPosition = new Vector2(-1, 0), Radius = 1 }, true),
            (new Circle{ CenterPosition = new Vector2(1.01f, 0), Radius = 1 }, new Circle { CenterPosition = new Vector2(-1, 0), Radius = 1 }, false),
        };

        private static IEnumerable<(Sphere a, Sphere b, bool expected)> SpheresTestCases => new[]
        {
            (new Sphere{ Radius = 1 }, new Sphere { Radius = 1 }, true),
            (new Sphere{ CenterPosition = new Vector3(0, 1, 0), Radius = 1 }, new Sphere { Radius = 1 }, true),
            (new Sphere{ CenterPosition = new Vector3(0, 0, 1), Radius = 1 }, new Sphere{ CenterPosition = new Vector3(0, 0, -1), Radius = 1 }, true),
            (new Sphere{ CenterPosition = new Vector3(0, 0, 1), Radius = 0.999f }, new Sphere{ CenterPosition = new Vector3(0, 0, -1), Radius = 1 }, false),
        };

        private static IEnumerable<(RectangleF a, RectangleF b, bool expected)> RectanglesTestCases => new[]
        {
            (new RectangleF(-2, -1, 1, 2), new RectangleF(0, 2, -2, 0), false),
            (new RectangleF(-2, -1, 1, 2), new RectangleF(-2, 2, -2, 0), false),
            (new RectangleF(-2, -1, 1, 2), new RectangleF(-2, 2, -2, 2), true),
            (new RectangleF(-2, 2, -2, 2), new RectangleF(-1, 1, -1, 1), true),
            (new RectangleF(-2, 2, -2, 2), new RectangleF(2, 3, -3, -2), true),
        };

        private static IEnumerable<(Box a, Box b, bool expected)> BoxesTestCases => new[]
        {
            (new Box(-2, -1, 1, 2, -2, -1), new Box(0, 2, -2, 0, 0, 2), false),
            (new Box(-2, -1, 1, 2, -2, -1), new Box(-2, 2, -2, 0, 0, 2), false),
            (new Box(-2, -1, 1, 2, -2, -1), new Box(-2, 2, -2, 2, -3, 0), true),
            (new Box(-2, 2, -2, 2, -2, 2), new Box(-1, 1, -1, 1, -1, 1), true),
            (new Box(-2, 2, -2, 2, -2, 2), new Box(2, 3, -3, -2, -3, -2), true),
        };

        [TestCaseSource(nameof(PointCircleTestCases))]
        public void IsPointInCircleTest((Vector2 point, Circle circle, bool expected) testCase) => Assert.AreEqual(testCase.expected, ElementaryCollisions.IsPointInCircle(testCase.point, testCase.circle));

        [TestCaseSource(nameof(PointTriangleTestCases))]
        public void IsPointInTriangleTest((Vector2 point, Triangle2 triangle, bool expected) testCase) => Assert.AreEqual(testCase.expected, ElementaryCollisions.IsPointInTriangle(testCase.point, testCase.triangle));

        [TestCaseSource(nameof(PointRectTestCases))]
        public void IsPointInRectangleTest((Vector2 point, RectangleF rect, bool expected) testCase) => Assert.AreEqual(testCase.expected, ElementaryCollisions.IsPointInRectangle(testCase.point, testCase.rect));

        [TestCaseSource(nameof(CirclesTestCases))]
        public void AreCirclesCollidingTest((Circle a, Circle b, bool expected) testCase) => Assert.AreEqual(testCase.expected, ElementaryCollisions.AreCirclesColliding(testCase.a, testCase.b));

        [TestCaseSource(nameof(SpheresTestCases))]
        public void AreSpheresCollidingTest((Sphere a, Sphere b, bool expected) testCase) => Assert.AreEqual(testCase.expected, ElementaryCollisions.AreSpheresColliding(testCase.a, testCase.b));

        [TestCaseSource(nameof(RectanglesTestCases))]
        public void AreRectanglesCollidingTest((RectangleF a, RectangleF b, bool expected) testCase) => Assert.AreEqual(testCase.expected, ElementaryCollisions.AreRectanglesColliding(testCase.a, testCase.b));

        [TestCaseSource(nameof(BoxesTestCases))]
        public void AreBoxesCollidingTest((Box a, Box b, bool expected) testCase) => Assert.AreEqual(testCase.expected, ElementaryCollisions.AreBoxesColliding(testCase.a, testCase.b));
    }
}