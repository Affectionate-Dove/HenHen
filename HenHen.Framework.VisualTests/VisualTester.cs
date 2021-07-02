// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input;
using HenHen.Framework.Screens;
using HenHen.Framework.UI;
using HenHen.Framework.VisualTests.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HenHen.Framework.VisualTests
{
    public class VisualTester : Screen, IInputListener<VisualTesterControls>
    {
        private readonly FillFlowContainer scenesList;
        private readonly ScreenStack scenesContainer;
        private readonly List<Type> sceneTypes;
        private readonly List<TestSceneButton> buttons;
        private readonly VisualTesterInputActionHandler inputActionHandler;
        private readonly SceneInputActionHandler sceneInputActionHandler;
        private int sceneIndex;

        public VisualTester(Inputs inputs)
        {
            inputActionHandler = new VisualTesterInputActionHandler(inputs);
            inputActionHandler.Propagator.Listeners.Add(this);

            sceneInputActionHandler = new SceneInputActionHandler(inputs);

            RelativeSizeAxes = Axes.Both;

            scenesList = CreateScenesList();
            scenesContainer = new ScreenStack { RelativeSizeAxes = Axes.Both };
            var leftContainer = CreateLeftContainer();
            AddChild(leftContainer);

            sceneTypes = new List<Type>();
            buttons = new List<TestSceneButton>();
            CreateAndAddButtons();

            if (sceneTypes.Count is 0)
            {
                leftContainer.AddChild(new SpriteText
                {
                    Text = "No test scenes.",
                    RelativeSizeAxes = Axes.Both,
                    AlignMiddle = true
                });
                return;
            }

            var rightContainer = new Container
            {
                Padding = new MarginPadding { Left = 200 },
                RelativeSizeAxes = Axes.Both
            };
            AddChild(rightContainer);
            rightContainer.AddChild(scenesContainer);

            if (sceneTypes.Count > 0)
                scenesContainer.Push(CreateVisualTestScene(sceneTypes[sceneIndex]));
            UpdateButtonsColors();
        }

        public bool OnActionPressed(VisualTesterControls action)
        {
            if (action == VisualTesterControls.NextScene)
                sceneIndex++;
            else if (action == VisualTesterControls.PreviousScene)
                sceneIndex--;
            else
                return false;

            ChangeScene();
            return true;
        }

        public void OnActionReleased(VisualTesterControls action)
        {
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (sceneIndex < sceneTypes.Count - 1 && (scenesContainer.CurrentScreen as VisualTestScene).IsSceneDone)
            {
                sceneIndex++;
                ChangeScene();
            }
            inputActionHandler.Update();
            sceneInputActionHandler.Update();
        }

        private static FillFlowContainer CreateScenesList() => new()
        {
            RelativeSizeAxes = Axes.Y,
            Size = new System.Numerics.Vector2(200, 1),
            Padding = new MarginPadding { Horizontal = 5, Vertical = 5 },
            Spacing = 5,
            Direction = Direction.Vertical
        };

        private VisualTestScene CreateVisualTestScene(Type type)
        {
            var visualTestScene = Activator.CreateInstance(type) as VisualTestScene;
            sceneInputActionHandler.Propagator.Listeners.Add(visualTestScene);
            return visualTestScene;
        }

        private void CreateAndAddButtons()
        {
            var visualTestSceneTypes = Assembly.GetEntryAssembly().GetTypes()
                .Where(type => type.IsSubclassOf(typeof(VisualTestScene)));

            foreach (var type in visualTestSceneTypes)
            {
                sceneTypes.Add(type);
                var button = new TestSceneButton(type)
                {
                    RelativeSizeAxes = Axes.X,
                    Size = new System.Numerics.Vector2(1, 20)
                };
                scenesList.AddChild(button);
                buttons.Add(button);
            }
        }

        private Container CreateLeftContainer()
        {
            var leftContainer = new Container
            {
                RelativeSizeAxes = Axes.Y,
                Size = new System.Numerics.Vector2(200, 1),
            };
            leftContainer.AddChild(new Rectangle
            {
                RelativeSizeAxes = Axes.Both,
                Color = new ColorInfo(40, 40, 40)
            });
            leftContainer.AddChild(scenesList);

            return leftContainer;
        }

        private void ChangeScene()
        {
            if (sceneTypes.Count == 0)
                return; // take no action if there are no test scenes

            // make sure the index is valid (loop around)
            if (sceneIndex >= sceneTypes.Count)
                sceneIndex = 0;
            else if (sceneIndex < 0)
                sceneIndex = sceneTypes.Count - 1;

            scenesContainer.Pop();
            sceneInputActionHandler.Propagator.Listeners.Clear();
            scenesContainer.Push(CreateVisualTestScene(sceneTypes[sceneIndex]));
            UpdateButtonsColors();
        }

        private void UpdateButtonsColors()
        {
            foreach (var button in buttons)
            {
                Console.WriteLine(button.Type.Name);
                if (button.Type == sceneTypes[sceneIndex])
                    button.Highlight();
                else
                    button.Unhighlight();
            }
        }

        private class TestSceneButton : Button<VisualTesterControls>
        {
            private static readonly ColorInfo highlightColor = new(100, 100, 100);
            private static readonly ColorInfo defaultColor = new(60, 60, 60);

            public Type Type { get; }

            public TestSceneButton(Type type)
            {
                Type = type;
                Text = type.Name.Replace("TestScene", null);
            }

            public void Highlight() => Color = highlightColor;

            public void Unhighlight() => Color = defaultColor;
        }
    }
}