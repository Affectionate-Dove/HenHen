// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Input;
using System.Collections.Generic;

namespace HenFwork.Tests.Input
{
    public class TestInputListener : IInputListener<TestAction>
    {
        public readonly HashSet<TestAction> HandledActions = new();
        public bool ReceivedPress { get; private set; }
        public bool ReceivedRelease { get; private set; }

        public bool OnActionPressed(TestAction action)
        {
            ReceivedPress = true;
            return HandledActions.Contains(action);
        }

        public void OnActionReleased(TestAction action) => ReceivedRelease = true;

        public void ResetFlags() => ReceivedPress = ReceivedRelease = false;
    }
}