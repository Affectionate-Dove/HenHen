// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.UI;

namespace HenHen.Visual.Screens.FileSelect
{
    public class FileSelectScreen : HenHenScreen
    {
        public FileSelectScreen()
        {
            AddChild(new SpriteText
            {
                RelativeSizeAxes = Axes.X,
                Anchor = new(0.5f, 0),
                Origin = new(0.5f, 0),
                TextAlignment = new(0.5f),
                Text = "File select",
                FontSize = 30,
                Size = new(1, 200)
            });
            AddChild(new Rectangle
            {
                Anchor = new(0.5f, 0.6f),
                Origin = new(0.5f),
                RelativeSizeAxes = Axes.Both,
                Size = new(0.7f, 0.6f)
            });
            var worldButtonsContainer = new WorldButtonsContainer
            {
                Anchor = new(0.5f, 0.6f),
                Origin = new(0.5f),
                RelativeSizeAxes = Axes.Both,
                Size = new(0.7f, 0.6f)
            };
            worldButtonsContainer.WorldSelected += OnWorldSelected;
            AddChild(worldButtonsContainer);
        }

        private void OnWorldSelected() => Push(new PlaceholderScreen("World loading screen"));
    }
}