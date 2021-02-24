using HenHen.Framework.Input;
using HenHen.Framework.Screens;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace HenHen.Framework
{
    public class Game
    {
        public Window Window { get; }

        public ScreenStack ScreenStack { get; }

        public InputManager InputManager { get; }

        public Game()
        {
            Window = new Window(new Vector2(600, 400), "HenHen");
            InputManager = CreateInputManager();
            ScreenStack = new ScreenStack();
        }

        protected virtual InputManager CreateInputManager() => new InputManager();

        /// <param name="timeDelta">In seconds.</param>
        public void Loop(float timeDelta)
        {
            Update(timeDelta);
            Draw();
        }

        private void Draw()
        {
            BeginDrawing();
            ClearBackground(Raylib_cs.Color.BLACK);
            ScreenStack.Render();
            OnRender();
            EndDrawing();
        }

        /// <param name="timeDelta">In seconds.</param>
        private void Update(float timeDelta)
        {
            ScreenStack.Size = Window.Size;
            ScreenStack.Update();
            InputManager.Update(timeDelta);
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnRender()
        {
        }
    }
}
