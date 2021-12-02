// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Input;
using HenFwork.Input;
using NUnit.Framework;

namespace HenFwork.Tests.Input
{
    public class InputActionHandlerTests
    {
        private FakeInputs inputs;
        private TestInputActionHandler actionHandler;

        [SetUp]
        public void Setup()
        {
            inputs = new FakeInputs();
            actionHandler = new TestInputActionHandler(inputs);
        }

        [Test]
        public void Test()
        {
            actionHandler.Update();

            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));

            inputs.PushKey(KeyboardKey.KEY_A);
            actionHandler.Update();
            Assert.IsTrue(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));
            Assert.AreEqual(1, actionHandler.ActiveInputActions.Count);

            inputs.ReleaseKey(KeyboardKey.KEY_A);
            actionHandler.Update();
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));

            inputs.PushKey(KeyboardKey.KEY_LEFT_CONTROL);
            actionHandler.Update();
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);

            inputs.PushKey(KeyboardKey.KEY_S);
            actionHandler.Update();
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsTrue(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));
            Assert.AreEqual(1, actionHandler.ActiveInputActions.Count);

            inputs.ReleaseKey(KeyboardKey.KEY_LEFT_CONTROL);
            actionHandler.Update();
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreAllKeysPressedForAction(TestAction.Action2));
        }
    }
}