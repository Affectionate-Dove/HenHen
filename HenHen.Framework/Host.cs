// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Framework
{
    public static class Host
    {
        public static void Run(Game game)
        {
            while (!Raylib_cs.Raylib.WindowShouldClose())
            {
                var elapsed = Raylib_cs.Raylib.GetFrameTime();
                game.Loop(elapsed);
            }
            Raylib_cs.Raylib.CloseWindow();
        }
    }
}