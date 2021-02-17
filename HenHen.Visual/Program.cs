using System.Numerics;

namespace HenHen.Visual
{
    internal class Program
    {
        private static void Main()
        {
            var game = new HenHenGame(new Window(new Vector2(600, 400), "HenHen"));

            while (!Raylib_cs.Raylib.WindowShouldClose())
                game.Loop();

            Raylib_cs.Raylib.CloseWindow();
        }
    }
}
