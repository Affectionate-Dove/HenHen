// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenHen.Framework.Input
{
    public class Inputs
    {
        public static float MouseWheelDelta => Raylib_cs.Raylib.GetMouseWheelMove();

        public virtual Vector2 MousePosition
        {
            get => Raylib_cs.Raylib.GetMousePosition();
            set => Raylib_cs.Raylib.SetMousePosition((int)value.X, (int)value.Y);
        }

        public virtual bool IsKeyDown(KeyboardKey key) => Raylib_cs.Raylib.IsKeyDown(key.ToRaylibKey());

        public virtual bool IsKeyUp(KeyboardKey key) => Raylib_cs.Raylib.IsKeyUp(key.ToRaylibKey());

        public virtual bool IsMouseButtonDown(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonDown(button.ToRaylibMouseButton());

        public virtual bool IsMouseButtonUp(MouseButton button) => Raylib_cs.Raylib.IsMouseButtonUp(button.ToRaylibMouseButton());
    }
}