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
            AddChild(new Rectangle
            {
                Size = new Vector2(160, 90),
                Origin = new Vector2(0.5f),
                Anchor = new Vector2(0.5f)
            });
            AddChild(new SpriteText
            {
                Text = "HHhhhhhhh",
                Position = new Vector2(100),
                Size = new Vector2(200),
                FontSize = 20,
                Color = new ColorInfo(255, 0, 0)
            });
        }
    }
}
