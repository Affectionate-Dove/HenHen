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
        };

        [TestCaseSource(nameof(PointCircleTestCases))]
        public void IsPointInCircleTest((Vector2 point, Circle circle, bool expected) testCase) => Assert.AreEqual(testCase.expected, ElementaryCollisions.IsPointInCircle(testCase.point, testCase.circle));

        [Test]
        public void IsPointInTriangleTest() => Assert.Inconclusive("Test not implemented");

        [Test]
        public void IsPointInRectangleTest() => Assert.Inconclusive("Test not implemented");

        [Test]
        public void AreCirclesCollidingTest() => Assert.Inconclusive("Test not implemented");

        [Test]
        public void AreSpheresCollidingTest() => Assert.Inconclusive("Test not implemented");
    }
}