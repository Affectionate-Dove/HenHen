// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input.UI;
using HenHen.Framework.Screens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private TestComponent component6Nested;
        private TestContainerComponent component4ContainerNested;
        private TestComponent component5DoubleNested;

        [SetUp]
        public void SetUp()
        {
            var screenStack = new ScreenStack();
            screen = new Screen();
            component1 = new TestComponent(1);
            component2 = new TestComponent(2);
            var componentContainer = new Container();
            componentContainer.AddChild(component3Nested = new TestComponent(3));
            componentContainer.AddChild(component4ContainerNested = new TestContainerComponent(4));
            component4ContainerNested.AddChild(component5DoubleNested = new TestComponent(5));
            componentContainer.AddChild(component6Nested = new TestComponent(6));
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
            component4ContainerNested.AcceptsFocus = false;
            component6Nested.AcceptsFocus = false;
            interfaceInputManager.FocusNextComponent();

            if (interfaceInputManager.FocusedComponents.Contains(component5DoubleNested))
                Assert.Fail("The component 5 is inside a disabled container component and should not be reachable.");
            Assert.IsEmpty(interfaceInputManager.FocusedComponents);
        }

        [Test]
        public void GetNextComponentTest()
        {
            for (var i = 0; i < 3; i++)
            {
                interfaceInputManager.FocusNextComponent();
                AssertContains(component1, interfaceInputManager.FocusedComponents);
                interfaceInputManager.FocusNextComponent();
                AssertContains(component2, interfaceInputManager.FocusedComponents);
                interfaceInputManager.FocusNextComponent();
                AssertContains(component3Nested, interfaceInputManager.FocusedComponents);
                interfaceInputManager.FocusNextComponent();
                AssertContains(new IInterfaceComponent<TestAction>[] { component4ContainerNested, component5DoubleNested }, interfaceInputManager.FocusedComponents);
                interfaceInputManager.FocusNextComponent();
                AssertContains(component6Nested, interfaceInputManager.FocusedComponents);
            }
        }

        [Test]
        public void GetNextComponentWithDisabledComponentTest()
        {
            component2.AcceptsFocus = false;
            for (var i = 0; i < 3; i++)
            {
                interfaceInputManager.FocusNextComponent();
                AssertContains(component1, interfaceInputManager.FocusedComponents);
                interfaceInputManager.FocusNextComponent();
                AssertContains(component3Nested, interfaceInputManager.FocusedComponents);
                interfaceInputManager.FocusNextComponent();
                AssertContains(new IInterfaceComponent<TestAction>[] { component4ContainerNested, component5DoubleNested }, interfaceInputManager.FocusedComponents);
                interfaceInputManager.FocusNextComponent();
                AssertContains(component6Nested, interfaceInputManager.FocusedComponents);
            }
        }

        [Test]
        public void GetNextComponentWithLayoutChangeTest()
        {
            interfaceInputManager.FocusNextComponent();
            AssertContains(component1, interfaceInputManager.FocusedComponents);
            screen.RemoveChild(component2);
            Assert.DoesNotThrow(() => interfaceInputManager.FocusNextComponent());
        }

        [Test]
        public void NextComponentActionTest()
        {
            var inputManager = new TestInputManager();
            var inputActionHandler = new TestInputActionHandler(inputManager);
            inputActionHandler.Propagator.Listeners.Add(interfaceInputManager);
            interfaceInputManager.NextComponentAction = TestAction.Action1;

            Assert.IsEmpty(interfaceInputManager.FocusedComponents);

            inputManager.SimulateKeyPress(Framework.Input.KeyboardKey.KEY_A);
            inputActionHandler.Update();
            inputManager.Update(1);

            inputManager.SimulateKeyRelease(Framework.Input.KeyboardKey.KEY_A);
            inputActionHandler.Update();
            inputManager.Update(1);

            AssertContains(component1, interfaceInputManager.FocusedComponents);
        }

        [Test]
        public void UnfocusTest()
        {
            interfaceInputManager.FocusNextComponent();
            AssertContains(component1, interfaceInputManager.FocusedComponents);
            interfaceInputManager.FocusNextComponent();
            AssertContains(component2, interfaceInputManager.FocusedComponents);
            interfaceInputManager.FocusNextComponent();
            AssertContains(component3Nested, interfaceInputManager.FocusedComponents);

            interfaceInputManager.Unfocus();
            Assert.IsEmpty(interfaceInputManager.FocusedComponents);

            interfaceInputManager.FocusNextComponent();
            AssertContains(component1, interfaceInputManager.FocusedComponents);
        }

        [Test]
        public void FocusComponentTest()
        {
            interfaceInputManager.FocusComponent(component3Nested);
            AssertContains(component3Nested, interfaceInputManager.FocusedComponents);

            interfaceInputManager.FocusNextComponent();
            AssertContains(new IInterfaceComponent<TestAction>[] { component5DoubleNested, component4ContainerNested }, interfaceInputManager.FocusedComponents);

            interfaceInputManager.FocusComponent(component2);
            AssertContains(component2, interfaceInputManager.FocusedComponents);

            var outsideComponent = new TestComponent(5);
            Assert.Throws<InvalidOperationException>(() => interfaceInputManager.FocusComponent(outsideComponent));
        }

        private static void AssertContains<T>(T expected, IEnumerable<T> actual) => Assert.IsTrue(actual.Contains(expected));

        private static void AssertContains<T>(IEnumerable<T> expected, IEnumerable<T> actual) => Assert.IsTrue(actual.Intersect(expected).Count() == actual.Count());

        private class TestContainerComponent : Container, IInterfaceComponent<TestAction>
        {
            private readonly int id;

            public bool AcceptsFocus { get; set; } = true;

            public TestContainerComponent(int id) => this.id = id;

            public override string ToString() => id.ToString();

            public void OnFocus()
            {
            }

            public void OnFocusLost()
            {
            }

            public bool OnActionPressed(TestAction action) => throw new NotImplementedException();

            public void OnActionReleased(TestAction action) => throw new NotImplementedException();
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

            public bool OnActionPressed(TestAction action) => throw new NotImplementedException();

            public void OnActionReleased(TestAction action) => throw new NotImplementedException();
        }
    }
}