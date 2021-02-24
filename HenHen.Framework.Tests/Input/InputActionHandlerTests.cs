using HenHen.Framework.Input;
using NUnit.Framework;

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
    }
}
