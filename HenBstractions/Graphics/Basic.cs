// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Numerics;
using Raylib_cs;

namespace HenBstractions.Graphics
{
    public static class Basic
    {
        public static void BeginDrawing() => Raylib.BeginDrawing();

        public static void BeginMode3D(Camera camera) => Raylib.BeginMode3D(camera.RaylibCamera);

        public static void EndMode3D() => Raylib.EndMode3D();

        public static bool WindowShouldClose() => Raylib.WindowShouldClose();

        public static float GetFrameTime() => Raylib.GetFrameTime();

        public static void CloseWindow() => Raylib.CloseWindow();

        public static void EndDrawing() => Raylib.EndDrawing();

        public static void ClearBackground(ColorInfo color) => Raylib.ClearBackground(color);

        public static void BeginTextureMode(RenderTexture renderTexture) => Raylib.BeginTextureMode(renderTexture.RenderTexture2D);

        public static void EndTextureMode() => Raylib.EndTextureMode();

        public static void EndScissorMode() => Raylib.EndScissorMode();

        public static void BeginScissorMode(RectangleF mask) => Raylib.BeginScissorMode((int)mask.Left, (int)mask.Top, (int)mask.Width, (int)mask.Height);
    }
}