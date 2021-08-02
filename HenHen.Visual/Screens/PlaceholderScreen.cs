// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Input;
using HenHen.Framework.Input.UI;
using HenHen.Framework.Screens;
using HenHen.Framework.UI;

namespace HenHen.Visual.Screens
{
    public class PlaceholderScreen : Screen, IPositionalInterfaceComponent
    {
        public bool AcceptsPositionalInput => true;

        public PlaceholderScreen(string text)
        {
            AddChild(new SpriteText
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = new(0.5f),
                Origin = new(0.5f),
                TextAlignment = new(0.5f),
                Text = text,
                FontSize = 30
            });
            AddChild(new SpriteText
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = new(0.5f),
                Origin = new(0.5f),
                TextAlignment = new(0.5f),
                Text = "placeholder",
                FontSize = 12,
                Offset = new(0, 20)
            });
            AddChild(new SpriteText
            {
                RelativeSizeAxes = Axes.X,
                Size = new(1, 20),
                Anchor = new(0.5f, 1),
                Origin = new(0.5f, 1),
                TextAlignment = new(0.5f),
                Text = "Press any mouse button to go back",
                FontSize = 12,
            });
        }

        public bool AcceptsPositionalButton(MouseButton button) => true;

        public void OnClick(MouseButton button) => Exit();

        public void OnHover()
        {
        }

        public void OnHoverLost()
        {
        }

        public void OnMousePress(MouseButton button)
        {
        }

        public void OnMouseRelease(MouseButton button)
        {
        }
    }
}