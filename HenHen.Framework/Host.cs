namespace HenHen.Framework
{
    public static class Host
    {
        public static void Run(Game game)
        {
            while (!Raylib_cs.Raylib.WindowShouldClose())
                game.Loop(Raylib_cs.Raylib.GetFrameTime());
            Raylib_cs.Raylib.CloseWindow();
        }
    }
}
