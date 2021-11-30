// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenFwork.Graphics2d;
using HenFwork.UI;

namespace HenHen.Visual.Screens.Settings
{
    /// <summary>
    ///     Allows the user to customize
    ///     various settings.
    /// </summary>
    public class SettingsScreen : HenHenScreen
    {
        private const int tabs_height = 40;

        public SettingsScreen()
        {
            var flow = new Container
            {
                Padding = new MarginPadding { Vertical = 20, Horizontal = 100 },
                RelativeSizeAxes = Axes.Both
            };
            AddChild(flow);

            flow.AddChild(GetTabsMockUp());
            flow.AddChild(GetContentMockUp());
        }

        private static Drawable GetTabsMockUp()
        {
            var tabsContainer = new Container
            {
                RelativeSizeAxes = Axes.X,
                Size = new(1, tabs_height),
            };
            tabsContainer.AddChild(new Rectangle { Color = new(50, 50, 50), RelativeSizeAxes = Axes.Both });

            var tabFlow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Spacing = 15,
                Direction = Direction.Horizontal,
                Padding = new MarginPadding { Horizontal = 20 }
            };
            tabsContainer.AddChild(tabFlow);

            void addTab(string name) => tabFlow.AddChild(new SpriteText
            {
                Text = name,
                Anchor = new(0, 0.5f),
                Origin = new(0, 0.5f),
                FontSize = 24
            });
            addTab("General");
            addTab("Graphics");
            addTab("Audio");
            addTab("Controls");
            return tabsContainer;
        }

        private static Drawable GetContentMockUp()
        {
            var contentContainer = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding { Top = tabs_height }
            };

            contentContainer.AddChild(new Rectangle { RelativeSizeAxes = Axes.Both, Color = new(40, 40, 40) });

            var flow = new FillFlowContainer
            {
                Direction = Direction.Vertical,
                Padding = new() { Vertical = 10 },
                Spacing = 10,
                RelativeSizeAxes = Axes.Both
            };
            contentContainer.AddChild(flow);

            flow.AddChild(GetSettingControlMockUp("Resolution", "1280x720"));
            flow.AddChild(GetSettingControlMockUp("Window mode", "Windowed"));
            flow.AddChild(GetSettingControlMockUp("Graphics preset", "High"));

            return contentContainer;
        }

        private static Drawable GetSettingControlMockUp(string settingName, string settingStatus)
        {
            var container = new Container
            {
                RelativeSizeAxes = Axes.X,
                Size = new(0.8f, 34),
                Anchor = new(0.5f, 0),
                Origin = new(0.5f, 0)
            };

            container.AddChild(new Rectangle { RelativeSizeAxes = Axes.Both, Color = new(80, 80, 80) });

            const int font_size = 20;

            container.AddChild(new SpriteText
            {
                RelativeSizeAxes = Axes.Both,
                Text = settingName,
                TextAlignment = new(0.5f),
                Size = new(0.5f, 1),
                FontSize = font_size
            });

            container.AddChild(new SpriteText
            {
                RelativeSizeAxes = Axes.Both,
                Text = settingStatus,
                TextAlignment = new(0.5f),
                Size = new(0.5f, 1),
                Anchor = new(1, 0),
                Origin = new(1, 0),
                FontSize = font_size
            });

            return container;
        }
    }
}