// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using Raylib_cs;

namespace HenBstractions.Graphics
{
    public static class Basic
    {
        public static bool WindowShouldClose() => Raylib.WindowShouldClose();

        public static float GetFrameTime() => Raylib.GetFrameTime();

        public static void CloseWindow() => Raylib.CloseWindow();
    }
}