// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenBstractions.Graphics;
using HenBstractions.Input;
using HenFwork.IO.Stores;
using HenFwork.Screens;
using HenFwork.UI;
using System.Numerics;

namespace HenFwork
{
    public class Game
    {
        private static TextureStore textureStore;
        private static ModelStore modelStore;

        public static TextureStore TextureStore => textureStore;
        public static ModelStore ModelStore => modelStore;
        public Inputs Inputs { get; }
        public Window Window { get; }
        public ScreenStack ScreenStack { get; }

        public Game()
        {
            Window = new Window(new Vector2(1280, 680), "HenHen");
            Inputs = CreateInputs();
            ScreenStack = new ScreenStack();
            textureStore = new TextureStore();
            modelStore = new ModelStore();

            SpriteText.DefaultFont = new Font("Resources/Fonts/OpenSans-SemiBold.ttf");
        }

        public void Loop(float elapsed)
        {
            Update(elapsed);
            Draw();
        }

        protected virtual Inputs CreateInputs() => new RealInputs();

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnRender()
        {
        }

        private void Draw()
        {
            Drawing.BeginDrawing();
            Drawing.ClearBackground(new(0, 0, 0));
            ScreenStack.Render();
            OnRender();
            Drawing.EndScissorMode();
            Drawing.EndDrawing();
        }

        private void Update(float elapsed)
        {
            ScreenStack.Size = Window.Size;
            ScreenStack.Update(elapsed);
            OnUpdate();
        }
    }
}