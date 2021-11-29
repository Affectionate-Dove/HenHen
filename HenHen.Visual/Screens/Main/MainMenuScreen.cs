// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Screens;
using HenHen.Framework.UI;
using HenHen.Visual.Inputs;
using HenHen.Visual.Screens.Credits;
using HenHen.Visual.Screens.FileSelect;
using HenHen.Visual.Screens.Settings;
using System;

namespace HenHen.Visual.Screens.Main
{
    public class MainMenuScreen : Screen
    {
        public MainMenuScreen()
        {
            AddChild(new Rectangle { RelativeSizeAxes = Axes.Both, Color = new(0, 10, 0) });
            AddChild(new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill,
                AutoFillModeProportions = true,
                Texture = Framework.Game.TextureStore.Get("Images/Backgrounds/MainMenu1.png")
            });
            AddChild(new SpriteText
            {
                Text = "HHhhhhhhh",
                Color = new ColorInfo(50, 200, 255)
            });
            AddChild(CreateButtonsContainer());
        }

        private static Button<MenuActions> CreateMainMenuButton(string text, Action action)
        {
            var button = new Button<MenuActions>()
            {
                RelativeSizeAxes = Axes.X,
                Size = new(1, 70),
                Text = text,
                Action = action,
                FontSize = 28,
                EnabledColors = new(new(159, 87, 0), new(63, 45, 0), new(255, 255, 255)),
                HoveredColors = new(new(204, 108, 0), null, null),
                FocusedColors = new(null, new(255, 178, 0), null),
                PressedColors = new(new(255, 163, 0), null, null),
                BorderThickness = 4
            };
            button.AcceptedActions.Add(MenuActions.Confirm);
            return button;
        }

        private Container CreateButtonsContainer()
        {
            var container = new FillFlowContainer
            {
                Size = new(320, 1),
                AutoSizeAxes = Axes.Y,
                Anchor = new(0.25f, 0.5f),
                Origin = new(0.5f),
                Direction = Direction.Vertical,
                Spacing = 45
            };

            container.AddChild(CreateMainMenuButton("File select", () => Push(new FileSelectScreen())));
            container.AddChild(CreateMainMenuButton("Credits", () => Push(new CreditsScreen())));
            container.AddChild(CreateMainMenuButton("Settings", () => Push(new SettingsScreen())));
            container.AddChild(CreateMainMenuButton("Exit", () => Environment.Exit(0)));

            return container;
        }
    }
}