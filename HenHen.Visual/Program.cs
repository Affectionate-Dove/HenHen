namespace HenHen.Visual
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Raylib_cs.Raylib.InitWindow(600, 400, "HenHen");
            Raylib_cs.Raylib.SetTargetFPS(60);
            var game = new HenHenGame();
            while (!Raylib_cs.Raylib.WindowShouldClose())
            {
                game.Loop();
            }

            Raylib_cs.Raylib.CloseWindow();
        }
    }
}
