using HenHen.Framework.Graphics2d;
using HenHen.Framework.Numerics;
using NUnit.Framework;
using System.Numerics;

namespace HenHen.Framework.Tests.Graphics2d
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
            container.Update();
            var absChildRenPos = absoluteChild.LayoutInfo.RenderPosition;
            Assert.AreEqual(new Vector2(300), absChildRenPos);
        }

        [Test]
        public void AbsoluteSizingTest()
        {
            absoluteChild.Anchor = Vector2.Zero;
            absoluteChild.Origin = Vector2.Zero;
            container.Update();
            var absChildRenSize = absoluteChild.LayoutInfo.RenderSize;
            Assert.AreEqual(new Vector2(200), absChildRenSize);
        }

        [Test]
        public void RelativePositioningTest()
        {
            relativeChild.Anchor = Vector2.Zero;
            relativeChild.Origin = Vector2.Zero;
            container.Update();
            var relChildRenPos = relativeChild.LayoutInfo.RenderPosition;
            Assert.AreEqual(new Vector2(1000), relChildRenPos);
        }

        [Test]
        public void RelativeSizingTest()
        {
            relativeChild.Anchor = Vector2.Zero;
            relativeChild.Origin = Vector2.Zero;
            container.Update();
            var relChildRenSize = relativeChild.LayoutInfo.RenderSize;
            Assert.AreEqual(new Vector2(200), relChildRenSize);
        }

        [Test]
        public void AnchorPositionTest()
        {
            absoluteChild.Anchor = new Vector2(0.3f);
            absoluteChild.Origin = Vector2.Zero;
            container.Update();
            var absChildRenPos = absoluteChild.LayoutInfo.RenderPosition;
            Assert.AreEqual(new Vector2(600), absChildRenPos);
        }

        [Test]
        public void OriginPositionTest()
        {
            absoluteChild.Anchor = new Vector2(0.5f);
            absoluteChild.Origin = new Vector2(0.5f);
            container.Update();
            var absChildRenRect = absoluteChild.LayoutInfo.RenderRect;
            Assert.AreEqual(new Vector2(800), absoluteChild.LayoutInfo.RenderPosition);
            Assert.AreEqual(new RectangleF
            {
                Left = 700,
                Top = 700,
                Width = 200,
                Height = 200
            }, absChildRenRect);
        }
    }
}