using HenHen.Framework.Graphics2d;
using NUnit.Framework;
using System.Numerics;

namespace HenHen.Framework.Tests.Graphics2d
{
    public class FillFlowContainerTests
    {
        private FillFlowContainer fillFlowContainer;
        private Rectangle child1;
        private Rectangle child2;

        [SetUp]
        public void SetUp()
        {
            fillFlowContainer = new FillFlowContainer();
            fillFlowContainer.AddChild(child1 = new Rectangle { Size = new Vector2(230) });
            fillFlowContainer.AddChild(child2 = new Rectangle
            {
                Size = new Vector2(50, 300)
            });
        }

        [Test]
        public void TestChildrenPositions()
        {
            fillFlowContainer.Update();
            Assert.AreEqual(new Vector2(0, 0), child1.GetRenderPosition());
            Assert.AreEqual(new Vector2(230, 0), child2.GetRenderPosition());

            fillFlowContainer.Direction = Direction.Vertical;
            fillFlowContainer.Update();
            Assert.AreEqual(new Vector2(0, 0), child1.GetRenderPosition());
            Assert.AreEqual(new Vector2(0, 230), child2.GetRenderPosition());
        }

        [Test]
        public void TestChildrenAddingRemoving()
        {
            var child1 = new Rectangle();
            var fillFlowContainer = new FillFlowContainer();
            fillFlowContainer.AddChild(child1);
            Assert.AreSame(fillFlowContainer, (child1.Parent as Container).Parent);
            fillFlowContainer.RemoveChild(child1);
            Assert.IsNull(child1.Parent);
            Assert.IsNull((child1.Parent as Container)?.Parent);
        }
    }
}
