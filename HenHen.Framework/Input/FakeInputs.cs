// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;
using System.Numerics;

namespace HenHen.Framework.Input
{
    public class FakeInputs : Inputs
    {
        private readonly HashSet<KeyboardKey> pressedKeys = new();
        private readonly HashSet<MouseButton> pressedButtons = new();
        private float mouseWheelDelta;
        public override Vector2 MousePosition { get; set; }

        public override float MouseWheelDelta => mouseWheelDelta;

        public void PushKey(KeyboardKey key) => pressedKeys.Add(key);

        public void ReleaseKey(KeyboardKey key) => pressedKeys.Remove(key);

        public void PushButton(MouseButton button) => pressedButtons.Add(button);

        public void ReleaseButton(MouseButton button) => pressedButtons.Remove(button);

        public override bool IsKeyDown(KeyboardKey key) => pressedKeys.Contains(key);

        public override bool IsKeyUp(KeyboardKey key) => !IsKeyDown(key);

        public override bool IsMouseButtonDown(MouseButton button) => pressedButtons.Contains(button);

        public override bool IsMouseButtonUp(MouseButton button) => !IsMouseButtonDown(button);

        public void SetMouseWheelDelta(float delta) => mouseWheelDelta = delta;
    }
}