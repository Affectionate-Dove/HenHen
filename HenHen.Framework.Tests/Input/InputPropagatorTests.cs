// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Input;
using NUnit.Framework;

namespace HenHen.Framework.Tests.Input
{
    public class InputPropagatorTests
    {
        private TestInputs inputs;
        private TestInputActionHandler inputActionHandler;

        [SetUp]
        public void SetUp()
        {
            inputs = new TestInputs();
            inputActionHandler = new TestInputActionHandler(inputs);
        }

        [Test]
        public void BasicTest()
        {
            var listener = new TestInputListener();
            inputActionHandler.Propagator.Listeners.Add(listener);

            SimulateFewSteps();

            inputs.SimulateKeyPress(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsTrue(listener.ReceivedPress);
            Assert.IsFalse(listener.ReceivedRelease);

            inputs.SimulateKeyRelease(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsFalse(listener.ReceivedRelease);
            SimulateFewSteps();

            listener.HandledActions.Add(TestAction.Action1);

            inputs.SimulateKeyPress(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsFalse(listener.ReceivedRelease);

            inputs.SimulateKeyRelease(KeyboardKey.KEY_A);
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

            inputs.SimulateKeyPress(KeyboardKey.KEY_LEFT_CONTROL);
            SimulateFewSteps();
            inputs.SimulateKeyPress(KeyboardKey.KEY_S);
            SimulateFewSteps();
            inputs.SimulateKeyRelease(KeyboardKey.KEY_S);
            SimulateFewSteps();
            inputs.SimulateKeyRelease(KeyboardKey.KEY_LEFT_CONTROL);
            SimulateFewSteps();

            Assert.IsTrue(foregroundListener.ReceivedPress);
            Assert.IsFalse(foregroundListener.ReceivedRelease);
            Assert.IsTrue(backgroundListener.ReceivedPress);
            Assert.IsTrue(backgroundListener.ReceivedRelease);
            backgroundListener.ResetFlags();
            foregroundListener.ResetFlags();

            inputs.SimulateKeyPress(KeyboardKey.KEY_A);
            SimulateFewSteps();
            inputs.SimulateKeyRelease(KeyboardKey.KEY_A);
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

            inputs.SimulateKeyPress(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsTrue(listener.ReceivedPress);
            Assert.IsFalse(listener.ReceivedRelease);
            listener.ResetFlags();

            inputs.SimulateKeyPress(KeyboardKey.KEY_A);
            SimulateFewSteps();

            Assert.IsFalse(listener.ReceivedPress);
            Assert.IsFalse(listener.ReceivedRelease);

            inputs.SimulateKeyRelease(KeyboardKey.KEY_A);
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