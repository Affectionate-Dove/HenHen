// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Graphics2d;
using HenHen.Framework.Screens;
using HenHen.Framework.UI;
using System.Numerics;

namespace HenHen.Visual.Screens.Main
{
    public class MainMenuScreen : Screen
    {
        public MainMenuScreen()
        {
            var logoSprite = new Sprite
            {
                Size = new Vector2(160, 160),
                Origin = new Vector2(0.5f),
                Anchor = new Vector2(0.5f),
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
        }
    }
}