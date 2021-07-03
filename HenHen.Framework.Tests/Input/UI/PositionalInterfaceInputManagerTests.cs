// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input;
using HenHen.Framework.Input.UI;
using HenHen.Framework.Screens;
using NUnit.Framework;
using System.Numerics;

namespace HenHen.Framework.Tests.Input.UI
{
    public class PositionalInterfaceInputManagerTests
    {
        private FakeInputs fakeInputs;

        private PositionalInterfaceInputManager manager;
        private TestComponent component1;
        private TestComponent component2;
        private TestComponent component3;

        [SetUp]
        public void SetUp()
        {
            var screenStack = new ScreenStack { Size = new(1000) };
            var screen = new Screen();
            screenStack.Push(screen);

            screen.AddChild(component1 = new TestComponent
            {
                Offset = new(50),
                Size = new(100)
            });

            screen.AddChild(component2 = new TestComponent
            {
                Offset = new(500),
                Size = new(500)
            });
            component2.AddChild(component3 = new TestComponent
            {
                Offset = new(200),
                Size = new(200)
            });

            screenStack.Update();

            fakeInputs = new();
            manager = new(fakeInputs, screenStack);
        }

        [Test]
        public void HoverPressReleaseClickTest()
        {
            fakeInputs.MousePosition = Vector2.Zero;
            manager.Update();
            Assert.IsFalse(component1.Hovered);

            fakeInputs.MousePosition = new(100);
            manager.Update();
            Assert.IsTrue(component1.Hovered);

            fakeInputs.PushButton(MouseButton.Left);
            manager.Update();
            Assert.AreEqual(1, component1.LeftPressCount);
            Assert.AreEqual(0, component1.LeftReleaseCount);
            Assert.AreEqual(0, component1.ClickCount);

            fakeInputs.ReleaseButton(MouseButton.Left);
            manager.Update();
            Assert.AreEqual(1, component1.LeftPressCount);
            Assert.AreEqual(1, component1.LeftReleaseCount);
            Assert.AreEqual(1, component1.ClickCount);

            fakeInputs.MousePosition = new(200);
            manager.Update();
            Assert.IsFalse(component1.Hovered);
        }

        [Test]
        public void LoseHoverDuringPressTest()
        {
            fakeInputs.MousePosition = new(100);
            fakeInputs.PushButton(MouseButton.Left);
            manager.Update();

            fakeInputs.MousePosition = new(200);
            manager.Update();
            Assert.Zero(component1.ClickCount);
            Assert.AreEqual(1, component1.LeftPressCount);
            Assert.AreEqual(1, component1.LeftReleaseCount);
        }

        [Test]
        public void HoverAndReleaseTest()
        {
            fakeInputs.PushButton(MouseButton.Left);
            manager.Update();
            fakeInputs.MousePosition = new(100);
            manager.Update();
            fakeInputs.ReleaseButton(MouseButton.Left);
            manager.Update();

            Assert.True(component1.Hovered);
            Assert.Zero(component1.ClickCount);
            Assert.AreEqual(0, component1.LeftPressCount);
            Assert.AreEqual(0, component1.LeftReleaseCount);
        }

        [Test]
        public void NestedComponentTest()
        {
            fakeInputs.MousePosition = new(600);
            manager.Update();
            Assert.IsTrue(component2.Hovered);
            Assert.IsFalse(component3.Hovered);

            fakeInputs.PushButton(MouseButton.Left);
            manager.Update();
            fakeInputs.ReleaseButton(MouseButton.Left);
            manager.Update();

            Assert.AreEqual(1, component2.ClickCount);
            Assert.AreEqual(0, component3.ClickCount);

            fakeInputs.MousePosition = new(700);
            manager.Update();
            Assert.IsTrue(component2.Hovered);
            Assert.IsTrue(component2.Hovered);

            fakeInputs.PushButton(MouseButton.Left);
            manager.Update();
            fakeInputs.ReleaseButton(MouseButton.Left);
            manager.Update();

            Assert.AreEqual(1, component2.ClickCount);
            Assert.AreEqual(1, component3.ClickCount);
        }

        private class TestComponent : Container, IPositionalInterfaceComponent
        {
            public bool AcceptsPositionalInput { get; set; } = true;
            public int ClickCount { get; private set; }
            public int HoverCount { get; private set; }
            public int HoverLostCount { get; private set; }
            public int LeftPressCount { get; private set; }
            public int RightPressCount { get; private set; }
            public int LeftReleaseCount { get; private set; }
            public int RightReleaseCount { get; private set; }
            public bool Hovered { get; private set; }

            public bool AcceptsPositionalButton(MouseButton button) => button is MouseButton.Left;

            public void OnClick(MouseButton button) => ClickCount++;

            public void OnHover()
            {
                HoverCount++;
                Hovered = true;
            }

            public void OnHoverLost()
            {
                HoverLostCount++;
                Hovered = false;
            }

            public void OnMousePress(MouseButton button)
            {
                if (button is MouseButton.Left)
                    LeftPressCount++;
                else if (button is MouseButton.Right)
                    RightPressCount++;
                else
                    throw new System.Exception();
            }

            public void OnMouseRelease(MouseButton button)
            {
                if (button is MouseButton.Left)
                    LeftReleaseCount++;
                else if (button is MouseButton.Right)
                    RightReleaseCount++;
                else
                    throw new System.Exception();
            }
        }
    }
}