// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Input;
using NUnit.Framework;

namespace HenFwork.Tests.Input
{
    public class InputPropagatorTests
    {
        private FakeInputs inputs;
        private TestInputActionHandler inputActionHandler;

        [SetUp]
        public void SetUp()
        {
            inputs = new FakeInputs();
            inputActionHandler = new TestInputActionHandler(inputs);
        }

        [Test]
        public void BasicTest()
        {
            var listener = new TestInputListener();
            inputActionHandler.Propagator.Listeners.Add(listener);

            SimulateFewSteps();

            inputs.PushKey(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsTrue(listener.ReceivedPress);
            Assert.IsFalse(listener.ReceivedRelease);

            inputs.ReleaseKey(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsFalse(listener.ReceivedRelease);
            SimulateFewSteps();

            listener.HandledActions.Add(TestAction.Action1);

            inputs.PushKey(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsFalse(listener.ReceivedRelease);

            inputs.ReleaseKey(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsTrue(listener.ReceivedRelease);

            inputActionHandler.Propagator.Listeners.Remove(listener);
            SimulateFewSteps();
        }

        [Test]
        public void HierarchyTest()
        {
            var backgroundListener = new TestInputListener();
            var foregroundListener = new TestInputListener();
            foregroundListener.HandledActions.Add(TestAction.Action1);
            backgroundListener.HandledActions.Add(TestAction.Action2);
            inputActionHandler.Propagator.Listeners.Add(backgroundListener);
            inputActionHandler.Propagator.Listeners.Add(foregroundListener);

            SimulateFewSteps();

            inputs.PushKey(KeyboardKey.KEY_LEFT_CONTROL);
            SimulateFewSteps();
            inputs.PushKey(KeyboardKey.KEY_S);
            SimulateFewSteps();
            inputs.ReleaseKey(KeyboardKey.KEY_S);
            SimulateFewSteps();
            inputs.ReleaseKey(KeyboardKey.KEY_LEFT_CONTROL);
            SimulateFewSteps();

            Assert.IsTrue(foregroundListener.ReceivedPress);
            Assert.IsFalse(foregroundListener.ReceivedRelease);
            Assert.IsTrue(backgroundListener.ReceivedPress);
            Assert.IsTrue(backgroundListener.ReceivedRelease);
            backgroundListener.ResetFlags();
            foregroundListener.ResetFlags();

            inputs.PushKey(KeyboardKey.KEY_A);
            SimulateFewSteps();
            inputs.ReleaseKey(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsTrue(foregroundListener.ReceivedPress);
            Assert.IsTrue(foregroundListener.ReceivedRelease);
            Assert.IsFalse(backgroundListener.ReceivedPress);
            Assert.IsFalse(backgroundListener.ReceivedRelease);

            inputActionHandler.Propagator.Listeners.Remove(backgroundListener);
            inputActionHandler.Propagator.Listeners.Remove(foregroundListener);
            SimulateFewSteps();
        }

        [Test]
        public void NoDoubledActionsTest()
        {
            var listener = new TestInputListener();
            listener.HandledActions.Add(TestAction.Action1);
            inputActionHandler.Propagator.Listeners.Add(listener);

            SimulateFewSteps();

            inputs.PushKey(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsTrue(listener.ReceivedPress);
            Assert.IsFalse(listener.ReceivedRelease);
            listener.ResetFlags();

            inputs.PushKey(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsFalse(listener.ReceivedPress);
            Assert.IsFalse(listener.ReceivedRelease);

            inputs.ReleaseKey(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsFalse(listener.ReceivedPress);
            Assert.IsTrue(listener.ReceivedRelease);

            inputActionHandler.Propagator.Listeners.Remove(listener);
            SimulateFewSteps();
        }

        private void SimulateFewSteps()
        {
            for (var i = 0; i < 3; i++)
            {
                inputActionHandler.Update();
            }
        }
    }
}