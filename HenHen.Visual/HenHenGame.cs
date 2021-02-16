using System;
using static Raylib_cs.Raylib;

namespace HenHen.Visual
{
    public class HenHenGame
    {
        public void Loop()
        {
            Update();
            Draw();
        }

        private void Draw()
        {
            BeginDrawing();
            ClearBackground(Raylib_cs.Color.RAYWHITE);
            DrawText("HHhhhhhhh", 100, 100, 20, Raylib_cs.Color.BLACK);
            EndDrawing();
        }

        private void Update()
        {

        }
    }
}
