using HenHen.Framework;
using HenHen.Framework.Screens;
using HenHen.Visual.Screens.Main;

namespace HenHen.Visual
{
    public class HenHenGame : Game
    {
        public HenHenGame() => ScreenStack.Push(new MainMenuScreen());
    }
}
