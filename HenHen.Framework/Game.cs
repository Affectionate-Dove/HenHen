// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.Input;
using HenHen.Framework.IO.Stores;
using HenHen.Framework.Screens;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace HenHen.Framework
{
    public class Game
    {
        private static TextureStore textureStore;
        private static InputManager inputManager;

        public static TextureStore TextureStore => textureStore;
        public static InputManager InputManager => inputManager;

        public Window Window { get; }
        public ScreenStack ScreenStack { get; }

        public Game()
        {
            Window = new Window(new Vector2(600, 400), "HenHen");
            inputManager = CreateInputManager();
            ScreenStack = new ScreenStack();
            textureStore = new TextureStore();
        }

        /// <param name="timeDelta">In seconds.</param>
        public void Loop(float timeDelta)
        {
            Update(timeDelta);
            Draw();
        }

        protected virtual InputManager CreateInputManager() => new();

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnRender()
        {
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
    }
}