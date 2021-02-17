using HenHen.Framework.Screens;
using HenHen.Visual.Screens.Main;
using static Raylib_cs.Raylib;

namespace HenHen.Visual
{
    public class HenHenGame
    {
        private readonly Window window;
        private readonly ScreenStack screenStack;

        public HenHenGame(Window window)
        {
            this.window = window;
            screenStack = new ScreenStack();
            screenStack.Push(new MainMenuScreen());
        }

        public void Loop()
        {
            Update();
            Draw();
        }

        private void Draw()
        {
            BeginDrawing();
            ClearBackground(Raylib_cs.Color.BLACK);
            DrawText("HHhhhhhhh", 100, 100, 20, Raylib_cs.Color.RAYWHITE);
            screenStack.Render();
            EndDrawing();
        }

        private void Update()
        {
            screenStack.Size = window.Size;
            screenStack.Update();
        }
    }
}
