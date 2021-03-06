﻿// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
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
        }

        public void Loop(float elapsed)
        {
            Update(elapsed);
            Draw();
        }

        protected virtual Inputs CreateInputs() => new RaylibInputs();

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
            EndScissorMode();
            EndDrawing();
        }

        private void Update(float elapsed)
        {
            ScreenStack.Size = Window.Size;
            ScreenStack.Update(elapsed);
            OnUpdate();
        }
    }
}