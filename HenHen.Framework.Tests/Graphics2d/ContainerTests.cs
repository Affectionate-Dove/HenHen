using HenHen.Framework.Graphics2d;
using NUnit.Framework;
using System.Numerics;

namespace HenHen.Framework.Tests.Graphics2d
{
    public class ContainerTests
    {
        private Container container;
        private Rectangle child1;
        private Rectangle child2;

        [SetUp]
        public void SetUp()
        {
            container = new Container
            {
                Position = new Vector2(100),
                Padding = new MarginPadding { Horizontal = 50, Vertical = 50 }
            };
            container.AddChild(child1 = new Rectangle { Size = new Vector2(230) });
            container.AddChild(child2 = new Rectangle
            {
                Position = new Vector2(100),
                Size = new Vector2(50, 300)
            });
        }

        [Test]
        public void TestAutoSizeAxes()
        {
            container.AutoSizeAxes = Axes.Both;
            Assert.AreEqual(new Vector2(230, 400), container.GetChildrenRenderSize());
            Assert.AreEqual(new System.Drawing.RectangleF(150, 150, 230, 230), child1.GetRenderRect());
            Assert.AreEqual(new System.Drawing.RectangleF(250, 250, 50, 300), child2.GetRenderRect());
        }
    }
}
