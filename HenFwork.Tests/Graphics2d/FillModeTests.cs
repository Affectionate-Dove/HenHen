// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Numerics;
using HenFwork.Graphics2d;
using NUnit.Framework;
using System.Collections.Generic;

namespace HenFwork.Tests.Graphics2d
{
    [TestOf(typeof(FillMode))]
    public class FillModeTests
    {
        private const float container_size = 8;
        private Container container;
        private Rectangle rectangle;

        public static IEnumerable<Case> GenerateCases()
        {
            yield return new(FillMode.Stretch, new RectangleF(0, container_size, container_size, 0));
            yield return new(FillMode.Fill, new RectangleF(0, container_size * 2, container_size, 0));
            yield return new(FillMode.Fit, new RectangleF(0, container_size, container_size * 0.5f, 0));
        }

        [SetUp]
        public void SetUp()
        {
            container = new Container
            {
                Size = new(container_size)
            };
            container.AddChild(rectangle = new Rectangle
            {
                Size = new(1),
                FillModeProportions = 2 / 1f,
                RelativeSizeAxes = Axes.Both
            });
        }

        [TestCaseSource(nameof(GenerateCases))]
        public void Test(Case c)
        {
            rectangle.FillMode = c.FillMode;
            container.Update(0);
            Assert.AreEqual(c.ExpectedLocalRect, rectangle.LayoutInfo.LocalRect);
        }

        public record Case(FillMode FillMode, RectangleF ExpectedLocalRect);
    }
}