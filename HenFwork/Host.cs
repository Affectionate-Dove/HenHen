// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;

namespace HenFwork
{
    public static class Host
    {
        public static void Run(Game game)
        {
            while (!Basic.WindowShouldClose())
            {
                var elapsed = Basic.GetFrameTime();
                game.Loop(elapsed);
            }
            Basic.CloseWindow();
        }
    }
}