// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Screens;
using HenHen.Framework.UI;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HenHen.Framework.VisualTests
{
    public class VisualTester : Screen
    {
        private readonly FillFlowContainer scenesList;
        private readonly ScreenStack scenesContainer;
        private readonly List<Type> sceneTypes;
        private int sceneIndex;

        public VisualTester()
        {
            RelativeSizeAxes = Axes.Both;
            var leftContainer = new Container
            {
                RelativeSizeAxes = Axes.Y,
                Size = new System.Numerics.Vector2(200, 1),
            };
            AddChild(leftContainer);
            leftContainer.AddChild(new Rectangle { RelativeSizeAxes = Axes.Both, Color = new ColorInfo(40, 40, 40) });
            leftContainer.AddChild(scenesList = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Y,
                Size = new System.Numerics.Vector2(200, 1),
                Padding = new MarginPadding { Horizontal = 5, Vertical = 5 },
                Spacing = 5,
                Direction = Direction.Vertical
            });

            sceneTypes = new List<Type>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(VisualTestScene)))
                {
                    sceneTypes.Add(type);
                    scenesList.AddChild(new TestSceneButton(type)
                    {
                        RelativeSizeAxes = Axes.X,
                        Size = new System.Numerics.Vector2(1, 20),
                        Color = new ColorInfo(60, 60, 60)
                    });
                }
            }

            var rightContainer = new Container
            {
                Padding = new MarginPadding { Left = 200 },
                RelativeSizeAxes = Axes.Both
            };
            AddChild(rightContainer);
            rightContainer.AddChild(scenesContainer = new ScreenStack
            {
                RelativeSizeAxes = Axes.Both
            });

            if (sceneTypes.Count > 0)
                scenesContainer.Push(Activator.CreateInstance(sceneTypes[sceneIndex]) as VisualTestScene);
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();
            if (Game.InputManager.IsKeyPressed(Input.KeyboardKey.KEY_PAGE_DOWN) || (sceneIndex < sceneTypes.Count - 1 && (scenesContainer.CurrentScreen as VisualTestScene).IsSceneDone))
            {
                scenesContainer.Pop();
                sceneIndex++;
                scenesContainer.Push(Activator.CreateInstance(sceneTypes[sceneIndex]) as VisualTestScene);
            }
        }

        private class TestSceneButton : Button
        {
            public Type Type { get; }

            public TestSceneButton(Type type)
            {
                Type = type;
                Text = type.Name;
            }
        }
    }
}