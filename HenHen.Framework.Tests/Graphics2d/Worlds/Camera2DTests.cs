// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d.Worlds;
using HenHen.Framework.Numerics;
using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Tests.Graphics2d.Worlds
{
    public class Camera2DTests
    {
        public static readonly IEnumerable<PositionToRenderingSpaceTestCase> PositionToRenderingSpaceTestCases = new PositionToRenderingSpaceTestCase[]
        {
            new(Vector2.Zero, 3, new Vector2(300), new Vector2(1, 0), new Vector2(250, 150)),
            new(Vector2.One, 3, new Vector2(300), Vector2.One, new Vector2(150)),
            new(new Vector2(5), 5, new Vector2(250), new Vector2(2.5f, 7.5f), Vector2.Zero),
            new(new Vector2(5), 4, new Vector2(250, 200), new Vector2(2.5f, 7.5f), new Vector2(0, -25))
        };

        public static readonly IEnumerable<AreaToRenderingSpaceTestCase> AreaToRenderingSpaceTestCases = new AreaToRenderingSpaceTestCase[]
        {
            new(Vector2.Zero, 3, new Vector2(300), new(-1.5f, 1.5f, -1.5f, 1.5f), new(0, 300, 300, 0)),
            new(Vector2.Zero, 3, new Vector2(300), new(-1, 1, -1, 1), new(50, 250, 250, 50)),
            new(Vector2.Zero, 3, new Vector2(200, 300), new(-1, 1, -1, 1), new(0, 200, 250, 50)),
        };

        public static readonly IEnumerable<VisibleAreaTestCase> VisibleAreaTestCases = new VisibleAreaTestCase[]
        {
            new(new Vector2(), 10, new Vector2(50), new RectangleF(-5, 5, -5, 5)),
            new(new Vector2(-5, 0), 10, new Vector2(50), new RectangleF(-10, 0, -5, 5)),
            new(new Vector2(0, 5), 10, new Vector2(50), new RectangleF(-5, 5, 0, 10)),

            new(new Vector2(), 10, new Vector2(25, 50), new RectangleF(-2.5f, 2.5f, -5, 5)),
            new(new Vector2(-5, 0), 10, new Vector2(25, 50), new RectangleF(-7.5f, -2.5f, -5, 5)),
            new(new Vector2(0, 5), 10, new Vector2(25, 50), new RectangleF(-2.5f, 2.5f, 0, 10)),
        };

        private readonly Camera2D camera = new();

        [TestCaseSource(nameof(VisibleAreaTestCases))]
        public void GetVisibleAreaTest(VisibleAreaTestCase tc)
        {
            camera.Target = tc.Target;
            camera.FovY = tc.FovY;
            var actualVisibleArea = camera.GetVisibleArea(tc.RenderingSpaceSize);
            Assert.AreEqual(tc.ExpectedVisibleArea, actualVisibleArea);
        }

        [TestCaseSource(nameof(PositionToRenderingSpaceTestCases))]
        public void PositionToRenderingSpaceTest(PositionToRenderingSpaceTestCase tc)
        {
            camera.Target = tc.Target;
            camera.FovY = tc.FovY;
            var actualRenderingSpacePosition = camera.PositionToRenderingSpace(tc.PositionInWorld, tc.RenderingSpaceSize);
            Assert.AreEqual(tc.ExpectedPositionInRenderingSpace, actualRenderingSpacePosition);
        }

        [TestCaseSource(nameof(AreaToRenderingSpaceTestCases))]
        public void AreaToRenderingSpaceTest(AreaToRenderingSpaceTestCase tc)
        {
            camera.Target = tc.Target;
            camera.FovY = tc.FovY;
            var actualRenderingSpacePosition = camera.AreaToRenderingSpace(tc.AreaInWorld, tc.RenderingSpaceSize);
            Assert.AreEqual(tc.ExpectedAreaInRenderingSpace, actualRenderingSpacePosition);
        }

        public record VisibleAreaTestCase(Vector2 Target, float FovY, Vector2 RenderingSpaceSize, RectangleF ExpectedVisibleArea);

        public record PositionToRenderingSpaceTestCase(Vector2 Target, float FovY, Vector2 RenderingSpaceSize, Vector2 PositionInWorld, Vector2 ExpectedPositionInRenderingSpace);

        public record AreaToRenderingSpaceTestCase(Vector2 Target, float FovY, Vector2 RenderingSpaceSize, RectangleF AreaInWorld, RectangleF ExpectedAreaInRenderingSpace);
    }
}