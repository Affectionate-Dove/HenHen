using HenHen.Framework.Input;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HenHen.Framework.Tests.Input
{
    public class InputActionHandlerTests
    {
        private TestInputManager inputManager;
        private TestInputActionHandler actionHandler;

        [SetUp]
        public void Setup()
        {
            inputManager = new TestInputManager();
            actionHandler = new TestInputActionHandler(inputManager);
        }

        [Test]
        public void Test()
        {
            inputManager.Update(0.02f);
            actionHandler.Update();

            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action2));

            inputManager.SimulateKeyPress(KeyboardKey.KEY_A);
            actionHandler.Update();
            Assert.IsTrue(actionHandler.AreActionKeysAllPressed(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action2));
            Assert.AreEqual(1, actionHandler.ActiveInputActions.Count);
            inputManager.Update(0.02f);

            inputManager.SimulateKeyRelease(KeyboardKey.KEY_A);
            actionHandler.Update();
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action2));
            inputManager.Update(0.02f);

            inputManager.SimulateKeyPress(KeyboardKey.KEY_LEFT_CONTROL);
            actionHandler.Update();
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action2));
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            inputManager.Update(0.02f);

            inputManager.SimulateKeyPress(KeyboardKey.KEY_S);
            actionHandler.Update();
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action1));
            Assert.IsTrue(actionHandler.AreActionKeysAllPressed(TestAction.Action2));
            Assert.AreEqual(1, actionHandler.ActiveInputActions.Count);
            inputManager.Update(0.02f);

            inputManager.SimulateKeyRelease(KeyboardKey.KEY_LEFT_CONTROL);
            actionHandler.Update();
            Assert.AreEqual(0, actionHandler.ActiveInputActions.Count);
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action1));
            Assert.IsFalse(actionHandler.AreActionKeysAllPressed(TestAction.Action2));
            inputManager.Update(0.02f);
        }

        private class TestInputActionHandler : InputActionHandler<TestAction>
        {
            public TestInputActionHandler(InputManager inputManager) : base(inputManager)
            {
            }

            public override Dictionary<TestAction, List<KeyboardKey>> CreateDefaultKeybindings() => new Dictionary<TestAction, List<KeyboardKey>>
            {
                { TestAction.Action1, new List<KeyboardKey> { KeyboardKey.KEY_A } },
                { TestAction.Action2, new List<KeyboardKey> { KeyboardKey.KEY_LEFT_CONTROL, KeyboardKey.KEY_S } },
            };
        }

        private enum TestAction
        {
            Action1,
            Action2
        }

        private class TestInputManager : InputManager
        {
            private readonly HashSet<KeyboardKey> justPressedKeys = new();
            private readonly HashSet<KeyboardKey> pressedKeys = new();

            public void SimulateKeyPress(KeyboardKey key)
            {
                justPressedKeys.Add(key);
                pressedKeys.Add(key);
            }

            public void SimulateKeyRelease(KeyboardKey key) => pressedKeys.Remove(key);

            public override bool IsKeyPressed(KeyboardKey key) => justPressedKeys.Contains(key);
            public override bool IsKeyDown(KeyboardKey key) => pressedKeys.Contains(key);
            public override bool IsKeyUp(KeyboardKey key) => !IsKeyDown(key);
            public override bool IsKeyReleased(KeyboardKey key) => throw new NotImplementedException();

            public override void Update(float timeDelta) => justPressedKeys.Clear();
        }
    }
}
