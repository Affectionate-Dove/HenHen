using HenHen.Framework.Graphics2d;
using HenHen.Framework.Screens;

namespace HenHen.Visual.Screens.Main
{
    public class MainMenuScreen : Screen
    {
        public MainMenuScreen()
        {
            AddChild(new Rectangle
            {
                Size = new System.Numerics.Vector2(160, 90),
                Origin = new System.Numerics.Vector2(0.5f),
                Anchor = new System.Numerics.Vector2(0.5f)
            });
        }
    }
}
