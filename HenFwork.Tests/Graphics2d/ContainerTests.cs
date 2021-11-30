// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Graphics2d;
using HenFwork.Numerics;
using NUnit.Framework;
using System.Numerics;

namespace HenFwork.Tests.Graphics2d
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
                Offset = new Vector2(100),
                Padding = new MarginPadding { Horizontal = 50, Vertical = 50 }
            };
            container.AddChild(child1 = new Rectangle { Size = new Vector2(230) });
            container.AddChild(child2 = new Rectangle
            {
                Offset = new Vector2(100),
                Size = new Vector2(50, 300)
            });
        }

        [Test]
        public void TestAutoSizeAxes()
        {
            container.AutoSizeAxes = Axes.Both;
            container.Update(0);
            Assert.AreEqual(new Vector2(230, 400), container.ContainerLayoutInfo.ChildrenRenderArea.Size);
            Assert.AreEqual(RectangleF.FromPositionAndSize(new(150), new(230), CoordinateSystem2d.YDown), child1.LayoutInfo.RenderRect);
            Assert.AreEqual(RectangleF.FromPositionAndSize(new(250), new(50, 300), CoordinateSystem2d.YDown), child2.LayoutInfo.RenderRect);
        }

        [Test]
        public void RemoveAllTest()
        {
            container.RemoveAll(d => d.Size.X > 100);
            Assert.False(container.Children.Contains(child1));
            Assert.True(container.Children.Contains(child2));
        }

        [Test]
        public void ClearTest()
        {
            container.Clear();
            Assert.Zero(container.Children.Count);
        }
    }
}