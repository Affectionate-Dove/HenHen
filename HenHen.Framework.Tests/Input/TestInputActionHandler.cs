using HenHen.Framework.Input;
using System.Collections.Generic;

namespace HenHen.Framework.Tests.Input
{
    public class TestInputActionHandler : InputActionHandler<TestAction>
    {
        public TestInputActionHandler(InputManager inputManager) : base(inputManager)
        {
        }

        protected override Dictionary<TestAction, List<KeyboardKey>> CreateDefaultKeybindings() => new Dictionary<TestAction, List<KeyboardKey>>
            {
                { TestAction.Action1, new List<KeyboardKey> { KeyboardKey.KEY_A } },
                { TestAction.Action2, new List<KeyboardKey> { KeyboardKey.KEY_LEFT_CONTROL, KeyboardKey.KEY_S } },
            };
    }

    public enum TestAction
    {
        Action1,
        Action2
    }
}
