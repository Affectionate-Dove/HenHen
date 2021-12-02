// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Numerics;

namespace HenBstractions.Input
{
    public class RealInputs : Inputs
    {
        public override float MouseWheelDelta => Raylib_cs.Raylib.GetMouseWheelMove();

        public override Vector2 MousePosition
        {
            get => Raylib_cs.Raylib.GetMousePosition();
            set => Raylib_cs.Raylib.SetMousePosition((int)value.X, (int)value.Y);
        }

        public override bool IsKeyDown(KeyboardKey key) => Raylib_cs.Raylib.IsKeyDown(key.ToRaylibKey());

        public override bool IsKeyUp(KeyboardKey key) => Raylib_cs.Raylib.IsKeyUp(key.ToRaylibKey());

        // TODO: Raylib somewhat doesn't support backwards and forwards and does at the same time
        public override bool IsMouseButtonDown(MouseButton button) => (int)button <= 2 && Raylib_cs.Raylib.IsMouseButtonDown(button.ToRaylibMouseButton());

        // TODO: like above
        public override bool IsMouseButtonUp(MouseButton button) => (int)button <= 2 && Raylib_cs.Raylib.IsMouseButtonUp(button.ToRaylibMouseButton());
    }
}