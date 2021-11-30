// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Graphics2d;
using HenFwork.Numerics;
using NUnit.Framework;
using System.Numerics;

namespace HenFwork.Tests.Graphics2d
{
    public class DrawableTests
    {
        private Container container;
        private Rectangle absoluteChild;
        private Rectangle relativeChild;

        [SetUp]
        public void Setup()
        {
            container = new Container
            {
                Size = new Vector2(1000),
                Offset = new Vector2(100)
            };
            container.AddChild(absoluteChild = new Rectangle
            {
                Offset = new Vector2(200),
                Size = new Vector2(200)
            });
            container.AddChild(relativeChild = new Rectangle
            {
                RelativePositionAxes = Axes.Both,
                Offset = new Vector2(0.9f),
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.2f)
            });
        }

        [Test]
        public void AbsolutePositioningTest()
        {
            absoluteChild.Anchor = Vector2.Zero;
            absoluteChild.Origin = Vector2.Zero;
            container.Update(0);
            var absChildRenPos = absoluteChild.LayoutInfo.RenderPosition;
            Assert.AreEqual(new Vector2(300), absChildRenPos);
        }

        [Test]
        public void AbsoluteSizingTest()
        {
            absoluteChild.Anchor = Vector2.Zero;
            absoluteChild.Origin = Vector2.Zero;
            container.Update(0);
            var absChildRenSize = absoluteChild.LayoutInfo.RenderSize;
            Assert.AreEqual(new Vector2(200), absChildRenSize);
        }

        [Test]
        public void RelativePositioningTest()
        {
            relativeChild.Anchor = Vector2.Zero;
            relativeChild.Origin = Vector2.Zero;
            container.Update(0);
            var relChildRenPos = relativeChild.LayoutInfo.RenderPosition;
            Assert.AreEqual(new Vector2(1000), relChildRenPos);
        }

        [Test]
        public void RelativeSizingTest()
        {
            relativeChild.Anchor = Vector2.Zero;
            relativeChild.Origin = Vector2.Zero;
            container.Update(0);
            var relChildRenSize = relativeChild.LayoutInfo.RenderSize;
            Assert.AreEqual(new Vector2(200), relChildRenSize);
        }

        [Test]
        public void AnchorPositionTest()
        {
            absoluteChild.Anchor = new Vector2(0.3f);
            absoluteChild.Origin = Vector2.Zero;
            container.Update(0);
            var absChildRenPos = absoluteChild.LayoutInfo.RenderPosition;
            Assert.AreEqual(new Vector2(600), absChildRenPos);
        }

        [Test]
        public void OriginPositionTest()
        {
            absoluteChild.Anchor = new Vector2(0.5f);
            absoluteChild.Origin = new Vector2(0.5f);
            container.Update(0);
            var absChildRenRect = absoluteChild.LayoutInfo.RenderRect;
            Assert.AreEqual(new Vector2(800), absoluteChild.LayoutInfo.RenderPosition);
            Assert.AreEqual(RectangleF.FromPositionAndSize(new(700), new(200), CoordinateSystem2d.YDown), absChildRenRect);
        }
    }
}