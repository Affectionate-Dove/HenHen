// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Screens;
using HenHen.Framework.UI;
using HenHen.Visual.Inputs;
using System;
using System.Numerics;

namespace HenHen.Visual.Screens.Main
{
    public class MainMenuScreen : Screen
    {
        public MainMenuScreen()
        {
            AddChild(new Rectangle { RelativeSizeAxes = Axes.Both, Color = new(0, 10, 0) });
            var logoSprite = new Sprite
            {
                Size = new Vector2(160, 160),
                Origin = new Vector2(0.5f),
                Anchor = new Vector2(0.75f),
                Texture = Framework.Game.TextureStore.Get("Images/logo.png")
            };
            base.AddChild(logoSprite);
            AddChild(new SpriteText
            {
                Text = "HHhhhhhhh",
                Offset = new Vector2(100),
                Size = new Vector2(200),
                FontSize = 20,
                Color = new ColorInfo(255, 0, 0)
            });
            AddChild(CreateButtonsContainer());
        }

        private Container CreateButtonsContainer()
        {
            var container = new FillFlowContainer
            {
                Size = new(300, 1),
                AutoSizeAxes = Axes.Y,
                Anchor = new(0.25f, 0.5f),
                Origin = new(0.5f),
                Direction = Direction.Vertical,
                Spacing = 20
            };

            container.AddChild(CreateMainMenuButton("File select"));
            container.AddChild(CreateMainMenuButton("Credits"));
            container.AddChild(CreateMainMenuButton("Settings"));

            var exitButton = CreateMainMenuButton("Exit");
            exitButton.Action = () => Environment.Exit(0);
            container.AddChild(exitButton);

            return container;
        }

        private Button<MenuActions> CreateMainMenuButton(string text)
        {
            var button = new Button<MenuActions>()
            {
                RelativeSizeAxes = Axes.X,
                Size = new(1, 30),
                Text = text,
                Action = () => Push(new PlaceholderScreen(text)),
                FontSize = 20,
            };
            button.AcceptedActions.Add(MenuActions.Confirm);
            return button;
        }
    }
}