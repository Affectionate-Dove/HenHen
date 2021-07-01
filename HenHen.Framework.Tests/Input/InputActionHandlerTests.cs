// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Input;
using NUnit.Framework;

namespace HenHen.Framework.Tests.Input
{
    public class InputActionHandlerTests
    {
        private TestInputs inputs;
        private TestInputActionHandler actionHandler;

        [SetUp]
        public void Setup()
        {
            inputs = new TestInputs();
            actionHandler = new TestInputActionHandler(inputs);
        }

        [Test]
        public void Test()
        {
            actionHandler.Update();

            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));

            inputs.SimulateKeyPress(KeyboardKey.KEY_A);
            actionHandler.Update();
            Assert.IsTrue(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));
            Assert.AreEqual(1, actionHandler.ActiveInputActions.Count);

            inputs.SimulateKeyRelease(KeyboardKey.KEY_A);
            actionHandler.Update();
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));

            inputs.SimulateKeyPress(KeyboardKey.KEY_LEFT_CONTROL);
            actionHandler.Update();
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);

            inputs.SimulateKeyPress(KeyboardKey.KEY_S);
            actionHandler.Update();
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsTrue(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));
            Assert.AreEqual(1, actionHandler.ActiveInputActions.Count);

            inputs.SimulateKeyRelease(KeyboardKey.KEY_LEFT_CONTROL);
            actionHandler.Update();
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));
        }
    }
}