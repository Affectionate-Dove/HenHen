using HenHen.Framework.Graphics2d;
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
                Position = new Vector2(100)
            };
            container.AddChild(absoluteChild = new Rectangle
            {
                Position = new Vector2(200),
                Size = new Vector2(200)
            });
            container.AddChild(relativeChild = new Rectangle
            {
                RelativePositionAxes = Axes.Both,
                Position = new Vector2(0.9f),
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.2f)
            });
        }

        [Test]
        public void TestAbsolutePositioning()
        {
            absoluteChild.Anchor = Vector2.Zero;
            absoluteChild.Origin = Vector2.Zero;
            var absChildRenPos = absoluteChild.GetRenderPosition();
            Assert.AreEqual(new Vector2(300), absChildRenPos);
        }

        [Test]
        public void TestAbsoluteSizing()
        {
            absoluteChild.Anchor = Vector2.Zero;
            absoluteChild.Origin = Vector2.Zero;
            var absChildRenSize = absoluteChild.GetRenderSize();
            Assert.AreEqual(new Vector2(200), absChildRenSize);
        }

        [Test]
        public void TestRelativePositioning()
        {
            relativeChild.Anchor = Vector2.Zero;
            relativeChild.Origin = Vector2.Zero;
            var relChildRenPos = relativeChild.GetRenderPosition();
            Assert.AreEqual(new Vector2(1000), relChildRenPos);
        }

        [Test]
        public void TestRelativeSizing()
        {
            relativeChild.Anchor = Vector2.Zero;
            relativeChild.Origin = Vector2.Zero;
            var relChildRenSize = relativeChild.GetRenderSize();
            Assert.AreEqual(new Vector2(200), relChildRenSize);
        }

        [Test]
        public void TestAnchorPosition()
        {
            absoluteChild.Anchor = new Vector2(0.3f);
            absoluteChild.Origin = Vector2.Zero;
            var absChildRenPos = absoluteChild.GetRenderPosition();
            Assert.AreEqual(new Vector2(600), absChildRenPos);
        }

        [Test]
        public void TestOriginPosition()
        {
            absoluteChild.Anchor = new Vector2(0.5f);
            absoluteChild.Origin = new Vector2(0.5f);
            var absChildRenRect = absoluteChild.GetRenderRect();
            Assert.AreEqual(new Vector2(800), absoluteChild.GetRenderPosition());
            Assert.AreEqual(new System.Drawing.RectangleF(700, 700, 200, 200), absChildRenRect);
        }
    }
}