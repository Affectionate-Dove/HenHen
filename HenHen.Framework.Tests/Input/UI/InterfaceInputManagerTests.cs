// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input.UI;
using HenHen.Framework.Screens;
using NUnit.Framework;

namespace HenHen.Framework.Tests.Input.UI
{
    [Timeout(1000)]
    public class InterfaceInputManagerTests
    {
        private InterfaceInputManager<TestAction> interfaceInputManager;
        private Screen screen;
        private TestComponent component1;
        private TestComponent component2;
        private TestComponent component3Nested;
        private TestComponent component4Nested;

        [SetUp]
        public void SetUp()
        {
            var screenStack = new ScreenStack();
            screen = new Screen();
            component1 = new TestComponent(1);
            component2 = new TestComponent(2);
            var componentContainer = new Container();
            componentContainer.AddChild(component3Nested = new TestComponent(3));
            componentContainer.AddChild(component4Nested = new TestComponent(4));
            interfaceInputManager = new(screenStack);
            screen.AddChild(component1);
            screen.AddChild(component2);
            screen.AddChild(componentContainer);
            screenStack.Push(screen);
        }

        [Test]
        public void HandleNoComponentsTest()
        {
            component1.AcceptsFocus = false;
            component2.AcceptsFocus = false;
            component3Nested.AcceptsFocus = false;
            component4Nested.AcceptsFocus = false;
            interfaceInputManager.FocusNextComponent();
            Assert.IsNull(interfaceInputManager.CurrentComponent);
        }

        [Test]
        public void GetNextComponentTest()
        {
            for (var i = 0; i < 3; i++)
            {
                interfaceInputManager.FocusNextComponent();
                Assert.AreEqual(component1, interfaceInputManager.CurrentComponent);
                interfaceInputManager.FocusNextComponent();
                Assert.AreEqual(component2, interfaceInputManager.CurrentComponent);
                interfaceInputManager.FocusNextComponent();
                Assert.AreEqual(component3Nested, interfaceInputManager.CurrentComponent);
                interfaceInputManager.FocusNextComponent();
                Assert.AreEqual(component4Nested, interfaceInputManager.CurrentComponent);
            }
        }

        [Test]
        public void GetNextComponentWithDisabledComponentTest()
        {
            component2.AcceptsFocus = false;
            for (var i = 0; i < 3; i++)
            {
                interfaceInputManager.FocusNextComponent();
                Assert.AreEqual(component1, interfaceInputManager.CurrentComponent);
                interfaceInputManager.FocusNextComponent();
                Assert.AreSame(component3Nested, interfaceInputManager.CurrentComponent);
                interfaceInputManager.FocusNextComponent();
                Assert.AreEqual(component4Nested, interfaceInputManager.CurrentComponent);
            }
        }

        [Test]
        public void GetNextComponentWithLayoutChangeTest()
        {
            interfaceInputManager.FocusNextComponent();
            Assert.AreEqual(component1, interfaceInputManager.CurrentComponent);
            screen.RemoveChild(component2);
            Assert.DoesNotThrow(() => interfaceInputManager.FocusNextComponent());
        }

        private class TestComponent : Drawable, IInterfaceComponent<TestAction>
        {
            private readonly int id;

            public bool AcceptsFocus { get; set; } = true;

            public TestComponent(int id) => this.id = id;

            public override string ToString() => id.ToString();

            public void OnFocus()
            {
            }

            public void OnFocusLost()
            {
            }

            public bool OnActionPressed(TestAction action) => throw new System.NotImplementedException();

            public void OnActionReleased(TestAction action) => throw new System.NotImplementedException();
        }
    }
}