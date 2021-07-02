// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input;
using HenHen.Framework.Input.UI;
using HenHen.Framework.Screens;
using HenHen.Framework.UI;
using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.VisualTests.Input.UI
{
    public class InterfaceInputManagerTestScene : VisualTestScene
    {
        private readonly InterfaceInputManager<TestAction> interfaceInputManager;
        private readonly TestInputActionHandler inputActionHandler;

        public InterfaceInputManagerTestScene()
        {
            inputActionHandler = new TestInputActionHandler(new RaylibInputs());
            var screenStack = new ScreenStack
            {
                Size = new Vector2(500, 400),
                Offset = new(100)
            };
            AddChild(screenStack);
            var screen1 = new Screen();
            screenStack.Push(screen1);
            screen1.AddChild(new Rectangle { RelativeSizeAxes = Axes.Both, Color = new(100, 200, 50) });
            var mainFillFlowContainer = new TestFillFlowContainer(0.6f, 400, 0);
            screen1.AddChild(mainFillFlowContainer);
            var header = new TestFillFlowContainer(0.5f, 70, 2);
            mainFillFlowContainer.AddChildToFlowContainer(header);

            var horizontalContainer = new TestFillFlowContainer(0.5f, 300, 0);
            horizontalContainer.FillFlowContainer.Direction = Direction.Horizontal;
            var leftContainer = new TestFillFlowContainer(0.4f, 1, 3) { RelativeSizeAxes = Axes.Y, Size = new(100, 1) };
            var rightContainer = new TestFillFlowContainer(0.4f, 1, 3) { RelativeSizeAxes = Axes.Y, Size = new(100, 1) };
            mainFillFlowContainer.AddChildToFlowContainer(horizontalContainer);
            horizontalContainer.AddChildToFlowContainer(leftContainer);
            horizontalContainer.AddChildToFlowContainer(rightContainer);

            interfaceInputManager = new InterfaceInputManager<TestAction>(screenStack, TestAction.Next);
            inputActionHandler.Propagator.Listeners.Add(interfaceInputManager);
        }

        protected override void OnUpdate()
        {
            inputActionHandler.Update();
            base.OnUpdate();
        }

        private class TestFillFlowContainer : Container, IInterfaceComponent<TestAction>
        {
            private readonly Rectangle background;
            private readonly byte v;

            public FillFlowContainer FillFlowContainer { get; }

            public bool AcceptsFocus => true;

            public TestFillFlowContainer(float brightness, int height, int buttonAmount)
            {
                Size = new(1, height);
                FillFlowContainer = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Direction = Direction.Vertical,
                    Padding = new() { Horizontal = 10, Vertical = 10 },
                    Spacing = 10
                };
                RelativeSizeAxes = Axes.X;
                v = (byte)(brightness * 255);
                AddChild(background = new Rectangle { RelativeSizeAxes = Axes.Both, Color = new(v, v, v) });
                AddChild(FillFlowContainer);
                for (var i = 0; i < buttonAmount; i++)
                    FillFlowContainer.AddChild(new TestButton(i + 1, brightness - 0.1f));
            }

            public void AddChildToFlowContainer(Drawable drawable) => FillFlowContainer.AddChild(drawable);

            public void OnFocus() => background.Color = new(v, v, 0);

            public void OnFocusLost() => background.Color = new(v, v, v);

            public bool OnActionPressed(TestAction action) => false;

            public void OnActionReleased(TestAction action) => throw new System.NotImplementedException();
        }

        private class TestButton : Button<TestAction>
        {
            private readonly byte v;

            public TestButton(int id, float brightness)
            {
                v = (byte)(brightness * 255);
                Color = new(v, v, v);
                RelativeSizeAxes = Axes.X;
                Size = new(1, 20);
                Text = id.ToString();
            }

            public override void OnFocus() => Color = new(0, v, v);

            public override void OnFocusLost() => Color = new(v, v, v);
        }

        private class TestInputActionHandler : InputActionHandler<TestAction>
        {
            public TestInputActionHandler(Inputs inputs) : base(inputs)
            {
            }

            protected override Dictionary<TestAction, List<KeyboardKey>> CreateDefaultKeybindings() => new()
            {
                { TestAction.Up, new() { KeyboardKey.KEY_UP } },
                { TestAction.Down, new() { KeyboardKey.KEY_DOWN } },
                { TestAction.Left, new() { KeyboardKey.KEY_LEFT } },
                { TestAction.Right, new() { KeyboardKey.KEY_RIGHT } },
                { TestAction.Next, new() { KeyboardKey.KEY_TAB } },
            };
        }

        private enum TestAction
        {
            Up,
            Left,
            Down,
            Right,
            Next
        }
    }
}