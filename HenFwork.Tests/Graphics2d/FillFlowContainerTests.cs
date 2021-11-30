// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Graphics2d;
using HenFwork.Numerics;
using NUnit.Framework;
using System.Numerics;

namespace HenFwork.Tests.Graphics2d
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
            fillFlowContainer.AddChild(child2 = new Rectangle { Size = new Vector2(50, 300) });
        }

        [Test]
        public void ChildrenPositionsTest()
        {
            fillFlowContainer.Update(0);
            Assert.AreEqual(new Vector2(0, 0), child1.LayoutInfo.RenderPosition);
            Assert.AreEqual(new Vector2(230, 0), child2.LayoutInfo.RenderPosition);

            fillFlowContainer.Direction = Direction.Vertical;
            fillFlowContainer.Update(0);
            Assert.AreEqual(new Vector2(0, 0), child1.LayoutInfo.RenderPosition);
            Assert.AreEqual(new Vector2(0, 230), child2.LayoutInfo.RenderPosition);
        }

        [Test]
        public void ChildrenAddingRemovingTest()
        {
            var child1 = new Rectangle();
            var fillFlowContainer = new FillFlowContainer();
            fillFlowContainer.AddChild(child1);
            Assert.AreSame(fillFlowContainer, (child1.Parent as Container).Parent);
            fillFlowContainer.RemoveChild(child1);
            Assert.IsNull(child1.Parent);
            Assert.IsNull((child1.Parent as Container)?.Parent);
        }

        [Test]
        public void MenuLayoutTest()
        {
            var screen = new Container
            {
                Size = new Vector2(1920, 1080)
            };
            var buttonList = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Y,
                Size = new Vector2(300, 1),
                Spacing = 10,
                Direction = Direction.Vertical
            };
            screen.AddChild(buttonList);

            var button1 = new Rectangle { RelativeSizeAxes = Axes.X, Size = new Vector2(1, 40) };
            var button2 = new Rectangle { RelativeSizeAxes = Axes.X, Size = new Vector2(1, 40) };
            var button3 = new Rectangle { RelativeSizeAxes = Axes.X, Size = new Vector2(1, 40) };

            buttonList.AddChild(button1);
            buttonList.AddChild(button2);
            buttonList.AddChild(button3);

            screen.Update(0);

            Assert.AreEqual(new RectangleF(0, 300, 40, 0), button1.LayoutInfo.RenderRect);
            Assert.AreEqual(new RectangleF(0, 300, 90, 50), button2.LayoutInfo.RenderRect);
            Assert.AreEqual(new RectangleF(0, 300, 140, 100), button3.LayoutInfo.RenderRect);
        }

        [Test]
        public void RemoveAllTest()
        {
            fillFlowContainer.RemoveAll(d => d.Size.X > 100);
            Assert.False(fillFlowContainer.Contains(child1));
            Assert.True(fillFlowContainer.Contains(child2));
        }

        [Test]
        public void ClearTest()
        {
            fillFlowContainer.Clear();
            Assert.Zero(fillFlowContainer.Count);
        }
    }
}