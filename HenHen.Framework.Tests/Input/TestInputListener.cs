using HenHen.Framework.Input;
using System.Collections.Generic;

namespace HenHen.Framework.Tests.Input
{
    public class TestInputListener : IInputListener<TestAction>
    {
        public bool ReceivedPress { get; private set; }
        public bool ReceivedRelease { get; private set; }

        public readonly HashSet<TestAction> HandledActions = new();

        public bool OnActionPressed(TestAction action)
        {
            ReceivedPress = true;
            return HandledActions.Contains(action);
        }

        public void OnActionReleased(TestAction action) => ReceivedRelease = true;

        public void ResetFlags() => ReceivedPress = ReceivedRelease = false;
    }
}
